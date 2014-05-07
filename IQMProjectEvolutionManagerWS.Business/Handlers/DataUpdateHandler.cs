// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataUpdateHandler.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The data update handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.Interfaces.Services;

    using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
    using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;
    using IQMProjectEvolutionManagerWS.Business.Utility;
    using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;

    using Ninject;

    /// <summary>
    /// The data update handler.
    /// </summary>
    public static class DataUpdateHandler
    {
        /// <summary>
        /// The dependency resolver
        /// </summary>
        private static IDependencyResolver dependencyResolver;

        /// <summary>
        /// The notify handler
        /// </summary>
        private static NotifyHandler notifyHandler;

        /// <summary>
        /// The run.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        /// <param name="notifyHandler">
        /// The notify Handler.
        /// </param>
        public static void Run(IDependencyResolver dependencyResolver, NotifyHandler notifyHandler)
        {
            DataUpdateHandler.dependencyResolver = dependencyResolver;
            DataUpdateHandler.notifyHandler = notifyHandler;

            InsertReleaseTypes();
            InsertReleaseStatusTypes();
            InsertStaffMembers();
            InsertReleasesByPreference();
        }

        /// <summary>
        /// Inserts the projects for release.
        /// </summary>
        /// <param name="release">The release.</param>
        /// <param name="domainRelease">The domain release.</param>
        private static void InsertProjectsForRelease(Data.Release release, ref Release domainRelease)
        {
            var releaseOfProject =
                dependencyResolver.GetKernel().Get<IOnTimeReleaseService>().GetReleaseOfProject(release);
            var projectsAssociatedWithRelease =
                dependencyResolver.GetKernel()
                    .Get<IOnTimeReleaseProjectService>()
                    .GetAssociatedProjects(releaseOfProject);

            foreach (var domainProject in from project in projectsAssociatedWithRelease
                                          where project != null
                                          select
                                              new Project
                                                  {
                                                      OnTimeId = project.ProjectId,
                                                      Name = project.Name,
                                                      IsActive = project.IsActive
                                                  })
            {
                InsertReleaseProjectForRelease(domainProject, ref domainRelease);

                dependencyResolver.GetKernel().Get<IProjectService>().Update(domainProject);
            }
        }

        /// <summary>
        /// Inserts the release projects for release.
        /// </summary>
        /// <param name="domainProject">The domain project.</param>
        /// <param name="domainRelease">The domain release.</param>
        private static void InsertReleaseProjectForRelease(Project domainProject, ref Release domainRelease)
        {
            if (domainRelease == null || domainProject == null)
            {
                return;
            }

            var domainReleaseProject = new ReleaseProject
                                           {
                                               Release = domainRelease,
                                               Project = dependencyResolver.GetKernel().Get<IProjectService>().GetByOnTimeId(domainProject.OnTimeId) ?? domainProject
                                           };

            if (!dependencyResolver.GetKernel().Get<IReleaseProjectService>().InDatabaseByOnTimeId(domainReleaseProject))
            {
                domainRelease.ReleaseProjects.Add(domainReleaseProject);
            }
        }

        /// <summary>
        /// Inserts the statistics for release.
        /// </summary>
        /// <param name="release">The release.</param>
        /// <param name="domainRelease">The domain release.</param>
        private static void InsertStatisticsForRelease(Data.Release release, ref Release domainRelease)
        {
            if (release == null || domainRelease == null)
            {
                return;
            }

            var tasks = dependencyResolver.GetKernel().Get<IOnTimeTaskService>().GetByRelease(release);

            var releaseWorkLogs = new List<ReleaseWorkLog>();

            foreach (var task in tasks)
            {
                var statistics = CalculateStatistics(task);

                domainRelease.HoursWorked += statistics["hoursWorkedForTask"];
                domainRelease.HoursRemaining += statistics["hoursRemainingForTask"];
                domainRelease.OriginalEstimateForAllTasks += statistics["originalEstimateHours"];

                UpdateReleaseWorkLogs(task, ref releaseWorkLogs, statistics);
            }

            domainRelease.PercentageComplete = domainRelease.OriginalEstimateForAllTasks.Equals(0.0f)
                                                   ? 0 : domainRelease.HoursWorked / domainRelease.OriginalEstimateForAllTasks * 100;

            foreach (var releaseWorkLog in releaseWorkLogs.Where(releaseWorkLog => releaseWorkLog != null))
            {
                releaseWorkLog.Release = domainRelease;
                if (!dependencyResolver.GetKernel().Get<IReleaseWorkLogService>().Update(releaseWorkLog))
                {
                    domainRelease.ReleaseWorkLogs.Add(releaseWorkLog);
                }
            }
        }

        /// <summary>
        /// The calculate statistics.
        /// </summary>
        /// <param name="task">
        /// The task.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        private static Dictionary<string, float> CalculateStatistics(Data.Task task)
        {
            var hoursWorkedThisWeek = 0.0f;
            var hoursRemainingForTask = 0.0f;

            if (task.WorkLogs.Any())
            {
                var workLogs = task.WorkLogs.Where(wl => wl.WorkLogDateTime >= DateTime.Today.AddDays(-7));
                hoursWorkedThisWeek += workLogs.Sum(workLog => TimeUnitCalculator.GetHours(workLog.WorkDone, workLog.TimeUnitType));
            }

            var hoursWorkedForTask = (task.ActualUnitTypeId == 0)
                                       ? 0
                                       : TimeUnitCalculator.GetHours(task.ActualDuration, task.ActualTimeUnitType);

            var originalEstimateHours = (task.DurationUnitTypeId == 0)
                                        ? 0
                                        : TimeUnitCalculator.GetHours(task.EstimatedDuration, task.EstimatedTimeUnitType);

            if (!task.IsCompleted)
            {
                hoursRemainingForTask = (task.RemainingUnitTypeId == 0)
                                            ? 0
                                            : TimeUnitCalculator.GetHours(task.RemainingDuration, task.RemainingTimeUnitType);
            }

            var statistics = new Dictionary<string, float>
                                 {
                                     { "hoursWorkedThisWeek", hoursWorkedThisWeek },
                                     { "hoursWorkedForTask", hoursWorkedForTask },
                                     { "hoursRemainingForTask", hoursRemainingForTask },
                                     { "originalEstimateHours", originalEstimateHours }
                                 };

            return statistics;
        }

        /// <summary>
        /// Updates the release work logs.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="releaseWorkLogs">The release work logs.</param>
        /// <param name="statistics">The statistics.</param>
        private static void UpdateReleaseWorkLogs(Data.Task task, ref List<ReleaseWorkLog> releaseWorkLogs, Dictionary<string, float> statistics)
        {
            var taskUserId = -1;

            if (task == null || statistics == null)
            {
                return;
            }

            if (task.User != null)
            {
                taskUserId = task.User.UserId;
            }

            if (releaseWorkLogs.Any(rWLog => rWLog.Staff.OnTimeId == taskUserId))
            {
                releaseWorkLogs.Where(rWLog => rWLog.Staff.OnTimeId == taskUserId).ToList().ForEach(rWLog =>
                    {
                        rWLog.HoursRemainingOnRelease += task.IsCompleted ? 0 : statistics["hoursRemainingForTask"];
                        rWLog.HoursWorkedOnRelease += statistics["hoursWorkedForTask"];
                        rWLog.HoursWorkedOnReleaseInLastWeek += statistics["hoursWorkedThisWeek"];
                    });
            }
            else
            {
                var staffService = dependencyResolver.GetKernel().Get<IStaffService>();

                releaseWorkLogs.Add(new ReleaseWorkLog
                                        {
                                            Staff = staffService.GetByOnTimeId(taskUserId),
                                            HoursRemainingOnRelease = task.IsCompleted ? 0 : statistics["hoursRemainingForTask"],
                                            HoursWorkedOnRelease = statistics["hoursWorkedForTask"],
                                            HoursWorkedOnReleaseInLastWeek = statistics["hoursWorkedThisWeek"]
                                        });
            }
        }

        /// <summary>
        /// Inserts the releases by preference.
        /// </summary>
        private static void InsertReleasesByPreference()
        {
            var releaseType = dependencyResolver.GetKernel().Get<IOnTimeReleaseTypeService>().GetByName(ConfigurationManager.AppSettings["ReleaseType"]);
            var releaseStatusType = dependencyResolver.GetKernel().Get<IOnTimeReleaseStatusTypeService>().GetByNameAndReleaseType(ConfigurationManager.AppSettings["ReleaseStatusType"], releaseType.ReleaseTypeId);

            var matchingReleases = dependencyResolver.GetKernel().Get<IOnTimeReleaseService>().GetReleasesByCriteria(releaseType, releaseStatusType, false);

            foreach (var release in matchingReleases)
            {
                if (release == null)
                {
                    continue;
                }

                var domainRelease = new Release
                                        {
                                            OnTimeId = release.ReleaseId,
                                            ParentReleaseId = release.ParentReleaseId,
                                            Name = release.Name,
                                            IsActive = release.IsActive,
                                            DueDate = release.DueDate,
                                            ReleaseNotes = release.ReleaseNotes,
                                            ReleaseType = dependencyResolver.GetKernel().Get<IReleaseTypeService>().GetByOnTimeId(release.ReleaseTypeId),
                                            ReleaseStatusType = dependencyResolver.GetKernel().Get<IReleaseStatusTypeService>().GetByOnTimeId(release.ReleaseStatusTypeId),
                                        };

                InsertStatisticsForRelease(release, ref domainRelease);
                InsertProjectsForRelease(release, ref domainRelease);

                dependencyResolver.GetKernel().Get<IReleaseService>().InsertOrUpdate(domainRelease);

                notifyHandler.HandleUpdates(dependencyResolver.GetKernel().Get<IReleaseService>().GetByOnTimeId(domainRelease.OnTimeId));
            }
        }

        /// <summary>
        /// Inserts the staff members.
        /// </summary>
        private static void InsertStaffMembers()
        {
            var onTimeUsers = dependencyResolver.GetKernel().Get<IOnTimeUserService>().GetAll(false);

            onTimeUsers.Add(
                new Data.User { UserId = -1, FirstName = "Unknown", LastName = string.Empty, IsActive = true });

            foreach (var domainStaffMember in onTimeUsers.Select(user => new Staff
                                                                             {
                                                                                 OnTimeId = user.UserId,
                                                                                 FirstName = user.FirstName,
                                                                                 LastName = user.LastName,
                                                                                 IsActive = user.IsActive
                                                                             }))
            {
                dependencyResolver.GetKernel().Get<IStaffService>().InsertOrUpdate(domainStaffMember);
            }
        }

        /// <summary>
        /// Inserts the release types.
        /// </summary>
        private static void InsertReleaseTypes()
        {
            var releaseTypesOnOnTime = dependencyResolver.GetKernel().Get<IOnTimeReleaseTypeService>().GetAll();

            foreach (var domainReleaseType in releaseTypesOnOnTime.Select(releaseType => new ReleaseType
                                                                                             {
                                                                                                 OnTimeId = releaseType.ReleaseTypeId,
                                                                                                 Name = releaseType.Name
                                                                                             }))
            {
                dependencyResolver.GetKernel().Get<IReleaseTypeService>().InsertOrUpdate(domainReleaseType);
            }
        }

        /// <summary>
        /// Inserts the release status types.
        /// </summary>
        private static void InsertReleaseStatusTypes()
        {
            var releaseStatusTypesOnOnTime = dependencyResolver.GetKernel().Get<IOnTimeReleaseStatusTypeService>().GetAll();

            foreach (var domainReleaseStatusType in releaseStatusTypesOnOnTime.Select(releaseStatusType => new ReleaseStatusType
            {
                OnTimeId = releaseStatusType.ReleaseStatusTypeId,
                ReleaseTypeId = releaseStatusType.ReleaseTypeId,
                Name = releaseStatusType.Name
            }))
            {
                dependencyResolver.GetKernel().Get<IReleaseStatusTypeService>().InsertOrUpdate(domainReleaseStatusType);
            }
        }
    }
}