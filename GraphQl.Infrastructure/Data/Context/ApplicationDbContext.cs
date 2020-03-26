using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Job;
using GraphQl.Core.Entities.Users;
using GraphQl.Core.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQl.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Job> Job { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(ConfigureJob);
            modelBuilder.Entity<Employer>(ConfigureEmployer);
            modelBuilder.Entity<Applicant>(ConfigureApplicant);
            modelBuilder.Entity<JobApplication>(ConfigureJobApplications);
        }

        public static void ConfigureJob(EntityTypeBuilder<Job> jobBuilder)
        {
            jobBuilder.ToTable("Jobs");
            jobBuilder.Property(job => job.JobStatus).HasConversion<string>().HasDefaultValue(JobStatus.Draft);

            var jobApplicationsNavigation =
                jobBuilder.Metadata.FindNavigation(nameof(Core.Entities.Job.Job.JobApplications));
            jobApplicationsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        public static void ConfigureEmployer(EntityTypeBuilder<Employer> employerBuilder)
        {
            employerBuilder.ToTable("Employers");
        }

        public static void ConfigureApplicant(EntityTypeBuilder<Applicant> applicantBuilder)
        {
            applicantBuilder.ToTable("Applicants");
        }

        public static void ConfigureJobApplications(EntityTypeBuilder<JobApplication> jobApplicationsBuilder)
        {
            jobApplicationsBuilder.ToTable("JobApplicants");
            jobApplicationsBuilder.HasKey(jobApplications => new { jobApplications.JobId, jobApplications.ApplicantId });
            jobApplicationsBuilder.HasOne(jobApplications => jobApplications.Job)
                .WithMany(job => job.JobApplications)
                .HasForeignKey(jobApplications => jobApplications.JobId);

            jobApplicationsBuilder.HasOne(jobApplications => jobApplications.Applicant)
                .WithMany(applicant => applicant.JobApplications)
                .HasForeignKey(jobApplications => jobApplications.ApplicantId);
        }
    }
}
