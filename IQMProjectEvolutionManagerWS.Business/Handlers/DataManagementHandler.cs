using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using IQMProjectEvolutionManager.Core.Domain;
using System.Configuration;
using IQMProjectEvolutionManagerWS.Business.Utility;
using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    public class DataManagementHandler : IDataManagementHandler
    {
        /// <summary>
        /// The dependency resolver
        /// </summary>
        private readonly IDependencyResolver _dependencyResolver;
        /// <summary>
        /// The notify handler
        /// </summary>
        private readonly NotifyHandler _notifyHandler;

        /// <summary>
        /// Inserts the projects for release.
        /// </summary>
        /// <param name="release">The release.</param>
        /// <param name="domainRelease">The domain release.</param>
        private void InsertProjectsForRelease(Data.Release release, ref Release domainRelease)
        {
            var releaseOfProject = _dependencyResolver.GetKernel().Get<IOnTimeReleaseService>().GetReleaseOfProject(release);
            var projectsAssociatedWithRelease = _dependencyResolver.GetKernel().Get<IOnTimeReleaseProjectService>().GetAssociatedProjects(releaseOfProject);

            foreach (var project in projectsAssociatedWithRelease)
            {
                if (project != null)
                {
                    var domainProject = new Project
                    {
                        OnTimeId = project.ProjectId,
                        Name = project.Name,
                        IsActive = project.IsActive
                    };

                    InsertReleaseProjectForRelease(domainProject, ref domainRelease);

                    _dependencyResolver.GetKernel().Get<IProjectService>().Update(domainProject);
                }
            }
        }

        /// <summary>
        /// Inserts the release projects for release.
        /// </summary>
        /// <param name="domainProject">The domain project.</param>
        /// <param name="domainRelease">The domain release.</param>
        private void InsertReleaseProjectForRelease(Project domainProject, ref Release domainRelease)
        {
            if (domainRelease != null && domainProject != null)
            {
                var domainReleaseProject = new ReleaseProject
                {
                    Release = domainRelease,
                    Project = _dependencyResolver.GetKernel().Get<IProjectService>().GetByOnTimeId(domainProject.OnTimeId) ?? domainProject
                };

                if (!_dependencyResolver.GetKernel().Get<IReleaseProjectService>().InDatabaseByOnTimeId(domainReleaseProject))
                {
                    domainRelease.ReleaseProjects.Add(domainReleaseProject);
                }
            }
        }

        /// <summary>
        /// Inserts the statistics for release.
        /// </summary>
        /// <param name="release">The release.</param>
        /// <param name="domainRelease">The domain release.</param>
        private void InsertStatisticsForRelease(Data.Release release, ref Release domainRelease)
        {
            if (release != null && domainRelease != null)
            {
                var tasks = _dependencyResolver.GetKernel().Get<IOnTimeTaskService>().GetByRelease(release);

                var releaseWorkLogs = new List<ReleaseWorkLog>();

                foreach (var task in tasks)
                {
                    var statistics = CalculateStatistics(task);

                    domainRelease.HoursWorked += statistics["hoursWorkedForTask"];
                    domainRelease.HoursRemaining += statistics["hoursRemainingForTask"];
                    domainRelease.OriginalEstimateForAllTasks += statistics["originalEstimateHours"];

                    UpdateReleaseWorkLogs(task, ref releaseWorkLogs, statistics);
                }

                domainRelease.PercentageComplete = (domainRelease.OriginalEstimateForAllTasks.Equals(0.0f))
                    ? 0 : domainRelease.HoursWorked / domainRelease.OriginalEstimateForAllTasks * 100;

                foreach (var releaseWorkLog in releaseWorkLogs)
                {
                    if (releaseWorkLog != null)
                    {
                        releaseWorkLog.Release = domainRelease;
                        if (!_dependencyResolver.GetKernel().Get<IReleaseWorkLogService>().Update(releaseWorkLog))
                        {
                            domainRelease.ReleaseWorkLogs.Add(releaseWorkLog);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the statistics from task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        private Dictionary<string, float> CalculateStatistics(Data.Task task)
        {
            var hoursWorkedThisWeek = 0.0f;
            //var hoursWorkedForTask = 0.0f;
            var hoursRemainingForTask = 0.0f;
            //var originalEstimateHours = 0.0f;

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
                {"hoursWorkedThisWeek", hoursWorkedThisWeek},
                {"hoursWorkedForTask", hoursWorkedForTask},
                {"hoursRemainingForTask", hoursRemainingForTask},
                {"originalEstimateHours", originalEstimateHours}
            };

            return statistics;
        }

        /// <summary>
        /// Updates the release work logs.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="releaseWorkLogs">The release work logs.</param>
        /// <param name="statistics">The statistics.</param>
        private void UpdateReleaseWorkLogs(Data.Task task, ref List<ReleaseWorkLog> releaseWorkLogs, Dictionary<string, float> statistics)
        {
            var taskUserId = -1;

            if (task != null && statistics != null)
            {
                if (task.User != null)
                {
                    taskUserId = task.User.UserId;
                }

                if (releaseWorkLogs.Any(rWLog => rWLog.Staff.OnTimeId == taskUserId))
                {
                    releaseWorkLogs.Where(rWLog => rWLog.Staff.OnTimeId == taskUserId).ToList().ForEach(rWLog =>
                    {
                        rWLog.HoursRemainingOnRelease += (task.IsCompleted) ? 0 : statistics["hoursRemainingForTask"];
                        rWLog.HoursWorkedOnRelease += statistics["hoursWorkedForTask"];
                        rWLog.HoursWorkedOnReleaseInLastWeek += statistics["hoursWorkedThisWeek"];
                    });
                }
                else
                {
                    var staffService = _dependencyResolver.GetKernel().Get<IStaffService>();

                    releaseWorkLogs.Add(new ReleaseWorkLog
                    {
                        Staff = staffService.GetByOnTimeId(taskUserId),
                        HoursRemainingOnRelease = (task.IsCompleted) ? 0 : statistics["hoursRemainingForTask"],
                        HoursWorkedOnRelease = statistics["hoursWorkedForTask"],
                        HoursWorkedOnReleaseInLastWeek = statistics["hoursWorkedThisWeek"]
                    });
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataManagementHandler"/> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        /// <param name="notifyHandler">The notify handler.</param>
        public DataManagementHandler(IDependencyResolver dependencyResolver, NotifyHandler notifyHandler)
        {
            _dependencyResolver = dependencyResolver;
            _notifyHandler = notifyHandler;
        }

        /// <summary>
        /// Inserts the releases by preference.
        /// </summary>
        public void InsertReleasesByPreference()
        {
            var releaseType = _dependencyResolver.GetKernel().Get<IOnTimeReleaseTypeService>().GetByName(ConfigurationManager.AppSettings["ReleaseType"]);
            var releaseStatusType = _dependencyResolver.GetKernel().Get<IOnTimeReleaseStatusTypeService>().GetByNameAndReleaseType(ConfigurationManager.AppSettings["ReleaseStatusType"], releaseType.ReleaseTypeId);

            var matchingReleases = _dependencyResolver.GetKernel().Get<IOnTimeReleaseService>().GetReleasesByCriteria(releaseType, releaseStatusType, false);

            foreach (var release in matchingReleases)
            {
                if (release != null)
                {
                    var domainRelease = new Release
                    {
                        OnTimeId = release.ReleaseId,
                        ParentReleaseId = release.ParentReleaseId,
                        Name = release.Name,
                        IsActive = release.IsActive,
                        DueDate = release.DueDate,
                        ReleaseNotes = release.ReleaseNotes,
                        ReleaseType = _dependencyResolver.GetKernel().Get<IReleaseTypeService>().GetByOnTimeId(release.ReleaseTypeId),
                        ReleaseStatusType = _dependencyResolver.GetKernel().Get<IReleaseStatusTypeService>().GetByOnTimeId(release.ReleaseStatusTypeId),
                    };

                    InsertStatisticsForRelease(release, ref domainRelease);
                    InsertProjectsForRelease(release, ref domainRelease);

                    _dependencyResolver.GetKernel().Get<IReleaseService>().InsertOrUpdate(domainRelease);

                    _notifyHandler.HandleUpdates(_dependencyResolver.GetKernel().Get<IReleaseService>().GetByOnTimeId(domainRelease.OnTimeId));
                }
            }
        }

        /// <summary>
        /// Inserts the staff members.
        /// </summary>
        public void InsertStaffMembers()
        {
            var onTimeUsers = _dependencyResolver.GetKernel().Get<IOnTimeUserService>().GetAll(false);

            onTimeUsers.Add
                (
                new Data.User
                {
                    UserId = -1,
                    FirstName = "Unknown",
                    LastName = string.Empty,
                    IsActive = true
                }
                );

            foreach (var user in onTimeUsers)
            {
                var domainStaffMember = new Staff
                {
                    OnTimeId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive
                };

                _dependencyResolver.GetKernel().Get<IStaffService>().InsertOrUpdate(domainStaffMember);
            }
        }

        /// <summary>
        /// Inserts the release types.
        /// </summary>
        public void InsertReleaseTypes()
        {
            var releaseTypesOnOnTime = _dependencyResolver.GetKernel().Get<IOnTimeReleaseTypeService>().GetAll();

            foreach (var releaseType in releaseTypesOnOnTime)
            {
                var domainReleaseType = new ReleaseType
                {
                    OnTimeId = releaseType.ReleaseTypeId,
                    Name = releaseType.Name
                };

                _dependencyResolver.GetKernel().Get<IReleaseTypeService>().InsertOrUpdate(domainReleaseType);
            }
        }

        /// <summary>
        /// Inserts the release status types.
        /// </summary>
        public void InsertReleaseStatusTypes()
        {
            var releaseStatusTypesOnOnTime = _dependencyResolver.GetKernel().Get<IOnTimeReleaseStatusTypeService>().GetAll();

            foreach (var domainReleaseStatusType in releaseStatusTypesOnOnTime.Select(releaseStatusType => new ReleaseStatusType
            {
                OnTimeId = releaseStatusType.ReleaseStatusTypeId,
                ReleaseTypeId = releaseStatusType.ReleaseTypeId,
                Name = releaseStatusType.Name
            }))
            {
                _dependencyResolver.GetKernel().Get<IReleaseStatusTypeService>().InsertOrUpdate(domainReleaseStatusType);
            }
        }
    }
}