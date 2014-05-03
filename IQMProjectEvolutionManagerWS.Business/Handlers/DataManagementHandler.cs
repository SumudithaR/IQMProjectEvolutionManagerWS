using IQMProjectEvolutionManager.Core.Services;
using IQMProjectEvolutionManagerWS.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using IQMProjectEvolutionManagerWS.Core.Services;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.DomainWrappers;
using IQM.Common.Services;
using IQM.Common.Interfaces;
using IQMProjectEvolutionManager.Core.DomainWrappers.SearchDomainWrappers;
using IQMProjectEvolutionManagerWS.Core.Interfaces;
using IQM.Common.Repositories;
using System.Linq.Expressions;
using System.Configuration;
using IQMProjectEvolutionManagerWS.Business.Utility;
using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data.Interfaces.OnTimeModels;
using IQMProjectEvolutionManager.Core.Interfaces.Domain;
using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    public class DataManagementHandler : IDataManagementHandler
    {
        private readonly IDependencyResolver dependencyResolver;
        private readonly NotifyHandler notifyHandler;

        private void InsertProjectsForRelease(Data.Release release, ref Release domainRelease)
        {
            var releaseOfProject = dependencyResolver.GetKernel().Get<IOnTimeReleaseService>().GetReleaseOfProject(release);
            var projectsAssociatedWithRelease = dependencyResolver.GetKernel().Get<IOnTimeReleaseProjectService>().GetAssociatedProjects(releaseOfProject);

            foreach (var project in projectsAssociatedWithRelease)
            {
                if (project != null)
                {
                    var domainProject = new Project()
                    {
                        OnTimeId = project.ProjectId,
                        Name = project.Name,
                        IsActive = project.IsActive
                    };

                    InsertReleaseProjectForRelease(domainProject, ref domainRelease);

                    dependencyResolver.GetKernel().Get<IProjectService>().Update(domainProject);
                }
            }
        }

        private void InsertReleaseProjectForRelease(Project domainProject, ref Release domainRelease)
        {
            if (domainRelease != null && domainProject != null)
            {
                var domainReleaseProject = new ReleaseProject()
                {
                    Release = domainRelease,
                    Project = (dependencyResolver.GetKernel().Get<IProjectService>().GetByOnTimeId(domainProject.OnTimeId) == null)
                    ? domainProject : dependencyResolver.GetKernel().Get<IProjectService>().GetByOnTimeId(domainProject.OnTimeId)
                };

                if (!dependencyResolver.GetKernel().Get<IReleaseProjectService>().InDatabaseByOnTimeId(domainReleaseProject))
                {
                    domainRelease.ReleaseProjects.Add(domainReleaseProject);
                }
            }
        }

        private void InsertStatisticsForRelease(Data.Release release, ref Release domainRelease)
        {
            if (release != null && domainRelease != null)
            {
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

                domainRelease.PercentageComplete = (domainRelease.OriginalEstimateForAllTasks == 0)
                    ? 0 : domainRelease.HoursWorked / domainRelease.OriginalEstimateForAllTasks * 100;

                foreach (var releaseWorkLog in releaseWorkLogs)
                {
                    if (releaseWorkLog != null)
                    {
                        releaseWorkLog.Release = domainRelease;
                        if (!dependencyResolver.GetKernel().Get<IReleaseWorkLogService>().Update(releaseWorkLog))
                        {
                            domainRelease.ReleaseWorkLogs.Add(releaseWorkLog);
                        }
                    }
                }
            }
        }

        private Dictionary<string, float> CalculateStatistics(Data.Task task)
        {
            var hoursWorkedThisWeek = 0.0f;
            var hoursWorkedForTask = 0.0f;
            var hoursRemainingForTask = 0.0f;
            var originalEstimateHours = 0.0f;

            if (task.WorkLogs.Any())
            {
                var workLogs = task.WorkLogs.Where(wl => wl.WorkLogDateTime >= DateTime.Today.AddDays(-7));
                foreach (var workLog in workLogs)
                {
                    hoursWorkedThisWeek += TimeUnitCalculator.GetHours(workLog.WorkDone, workLog.TimeUnitType);
                }
            }

            hoursWorkedForTask = (task.ActualUnitTypeId == 0)
                                       ? 0
                                       : TimeUnitCalculator.GetHours(task.ActualDuration, task.ActualTimeUnitType);

            originalEstimateHours = (task.DurationUnitTypeId == 0)
                                        ? 0
                                        : TimeUnitCalculator.GetHours(task.EstimatedDuration, task.EstimatedTimeUnitType);

            if (!task.IsCompleted)
            {
                hoursRemainingForTask = (task.RemainingUnitTypeId == 0)
                                            ? 0
                                            : TimeUnitCalculator.GetHours(task.RemainingDuration, task.RemainingTimeUnitType);
            }

            var statistics = new Dictionary<string, float>();
            statistics.Add("hoursWorkedThisWeek", hoursWorkedThisWeek);
            statistics.Add("hoursWorkedForTask", hoursWorkedForTask);
            statistics.Add("hoursRemainingForTask", hoursRemainingForTask);
            statistics.Add("originalEstimateHours", originalEstimateHours);

            return statistics;
        }

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
                    var staffService = dependencyResolver.GetKernel().Get<IStaffService>();

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

        public DataManagementHandler(IDependencyResolver dependencyResolver, NotifyHandler notifyHandler)
        {
            this.dependencyResolver = dependencyResolver;
            this.notifyHandler = notifyHandler;
        }

        public void InsertReleasesByPreference()
        {
            var releaseType = dependencyResolver.GetKernel().Get<IOnTimeReleaseTypeService>().GetByName(ConfigurationManager.AppSettings["ReleaseType"]);
            var releaseStatusType = dependencyResolver.GetKernel().Get<IOnTimeReleaseStatusTypeService>().GetByNameAndReleaseType(ConfigurationManager.AppSettings["ReleaseStatusType"], releaseType.ReleaseTypeId);

            var matchingReleases = dependencyResolver.GetKernel().Get<IOnTimeReleaseService>().GetReleasesByCriteria(releaseType, releaseStatusType, false);

            foreach (var release in matchingReleases)
            {
                if (release != null)
                {
                    var domainRelease = new Release()
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
        }

        public void InsertStaffMembers()
        {
            var onTimeUsers = dependencyResolver.GetKernel().Get<IOnTimeUserService>().GetAll(false);

            onTimeUsers.Add
                (
                new Data.User()
                {
                    UserId = -1,
                    FirstName = "Unknown",
                    LastName = string.Empty,
                    IsActive = true
                }
                );

            foreach (var user in onTimeUsers)
            {
                var domainStaffMember = new Staff()
                {
                    OnTimeId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive
                };

                dependencyResolver.GetKernel().Get<IStaffService>().InsertOrUpdate(domainStaffMember);
            }
        }

        public void InsertReleaseTypes()
        {
            var releaseTypesOnOnTime = dependencyResolver.GetKernel().Get<IOnTimeReleaseTypeService>().GetAll();

            foreach (var releaseType in releaseTypesOnOnTime)
            {
                var domainReleaseType = new ReleaseType()
                {
                    OnTimeId = releaseType.ReleaseTypeId,
                    Name = releaseType.Name
                };

                dependencyResolver.GetKernel().Get<IReleaseTypeService>().InsertOrUpdate(domainReleaseType);
            }
        }

        public void InsertReleaseStatusTypes()
        {
            var releaseStatusTypesOnOnTime = dependencyResolver.GetKernel().Get<IOnTimeReleaseStatusTypeService>().GetAll();

            foreach (var releaseStatusType in releaseStatusTypesOnOnTime)
            {
                var domainReleaseStatusType = new ReleaseStatusType()
                {
                    OnTimeId = releaseStatusType.ReleaseStatusTypeId,
                    ReleaseTypeId = releaseStatusType.ReleaseTypeId,
                    Name = releaseStatusType.Name
                };

                dependencyResolver.GetKernel().Get<IReleaseStatusTypeService>().InsertOrUpdate(domainReleaseStatusType);
            }
        }
    }
}