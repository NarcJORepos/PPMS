using DAL.Configurations;
using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static Project;

namespace DAL.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Project> projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Directorate> directorates { get; set; }
        public DbSet<FundingAgency> FundingAgency { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Page> Pages { get; set; }

        public DbSet<OperationalObjective> OperationalObjectives { get; set; }

        public DbSet<ProjectParticipant> ProjectParticipants { get; set; }
        //-------------
        public DbSet<MainPillar> MainPillars { get; set; }
        public DbSet<SubIndicator> SubIndicators { get; set; }
        public DbSet<MeasurementMethod> MeasurementMethods { get; set; }
       // public DbSet<MeasurementIndicator> MeasurementIndicators { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===============================
            // علاقات Project
            // ===============================
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.CoordinatorInfo)
                .WithMany()
                .HasForeignKey(p => p.CoordinatorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.PillarLeadInfo)
                .WithMany()
                .HasForeignKey(p => p.PillarLeadID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.FundingAgency)
                .WithMany()
                .HasForeignKey(p => p.FundingAgencyID)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // Group Configuration
            // ===============================
            modelBuilder.ApplyConfiguration(new GroupConfiguration());

            // ===============================
            // Role Configuration
            // ===============================
            modelBuilder.Entity<Role>()
                .HasKey(r => r.ID);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Page)
                .WithMany()
                .HasForeignKey(r => r.PageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Group)
                .WithMany()
                .HasForeignKey(r => r.GroupID)
                .OnDelete(DeleteBehavior.Restrict);

            // ===============================
            // User Configuration
            // ===============================
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(u => u.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);


            // ===============================
            // Project Participant
            // ===============================
            modelBuilder.Entity<ProjectParticipant>()
                .HasKey(pp => pp.ProjectParticipantID);

            modelBuilder.Entity<ProjectParticipant>()
                .HasOne(pp => pp.Project)
                .WithMany()
                .HasForeignKey(pp => pp.ProjectID);

            modelBuilder.Entity<ProjectParticipant>()
                .HasOne(pp => pp.Employee)
                .WithMany()
                .HasForeignKey(pp => pp.EmployeeID);
        }

    }
}
