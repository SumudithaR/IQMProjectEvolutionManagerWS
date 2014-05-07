// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataCleanupHandler.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The data cleanup handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    using System;

    using IQMProjectEvolutionManager.Core.Interfaces.Services;

    using Ninject;

    using IDependencyResolver = IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver.IDependencyResolver;

    /// <summary>
    /// The data cleanup handler.
    /// </summary>
    public static class DataCleanupHandler
    {
        /// <summary>
        /// The dependency resolver.
        /// </summary>
        private static IDependencyResolver dependencyResolver;

        /// <summary>
        /// The filter days.
        /// </summary>
        private static int filterDays;

        /// <summary>
        /// The run.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        /// <param name="filterDays">
        /// The _filter days.
        /// </param>
        public static void Run(IDependencyResolver dependencyResolver, int filterDays)
        {
            DataCleanupHandler.dependencyResolver = dependencyResolver;
            DataCleanupHandler.filterDays = filterDays;

            CleanupReleases();
            CleanupProjects();
            CleanupReleaseStatusTypes();
            CleanupReleaseTypes();
            CleanupReleaseWorkLogs();
            CleanupStaffMembers();
        }

        /// <summary>
        /// The cleanup releases.
        /// </summary>
        private static void CleanupReleases()
        {
            var releaseService = dependencyResolver.GetKernel().Get<IReleaseService>();
            releaseService.Delete(releaseService.GetOlderByDays(filterDays));
        }

        /// <summary>
        /// The cleanup projects.
        /// </summary>
        private static void CleanupProjects()
        {
            var projectService = dependencyResolver.GetKernel().Get<IProjectService>();
            var test = projectService.GetOlderByDays(filterDays);
            projectService.Delete(test);
        }

        /// <summary>
        /// The cleanup release status types.
        /// </summary>
        private static void CleanupReleaseStatusTypes()
        {
            var releaseStatusTypeService = dependencyResolver.GetKernel().Get<IReleaseStatusTypeService>();
            releaseStatusTypeService.Delete(releaseStatusTypeService.GetOlderByDays(filterDays));
        }

        /// <summary>
        /// The cleanup release types.
        /// </summary>
        private static void CleanupReleaseTypes()
        {
            var releaseTypeService = dependencyResolver.GetKernel().Get<IReleaseTypeService>();
            releaseTypeService.Delete(releaseTypeService.GetOlderByDays(filterDays));
        }

        /// <summary>
        /// The cleanup release work logs.
        /// </summary>
        private static void CleanupReleaseWorkLogs()
        {
            var releaseWorkLogService = dependencyResolver.GetKernel().Get<IReleaseWorkLogService>();
            releaseWorkLogService.Delete(releaseWorkLogService.GetOlderByDays(filterDays));
        }

        /// <summary>
        /// The cleanup staff members.
        /// </summary>
        private static void CleanupStaffMembers()
        {
            var staffService = dependencyResolver.GetKernel().Get<IStaffService>();
            staffService.Delete(staffService.GetOlderByDays(filterDays));
        }
    }
}
