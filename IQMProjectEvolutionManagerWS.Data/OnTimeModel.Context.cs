﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnTime10Entities : DbContext
    {
        public OnTime10Entities()
            : base("name=OnTime10Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Project> Projects { get; set; }
        public DbSet<ReleaseProject> ReleaseProjects { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<ReleaseStatusType> ReleaseStatusTypes { get; set; }
        public DbSet<ReleaseType> ReleaseTypes { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TimeUnitType> TimeUnitTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; }
    }
}