using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Users;
using GraphQl.Core.Values;

namespace GraphQl.Core.Entities.Job
{
    public class Job : EntityBase
    {
        public int Id { get; private set; }
        public string EmployerId { get; private set; }
        public string JobTitle { get; private set; }
        public string JobDescription { get; private set; }
        public string JobLocation { get; private set; }
        public string JobRequirements { get; private set; }
        public int? AnnualSalary { get; private set; }
        public JobStatus JobStatus { get; private set; }
        public List<JobApplication> JobApplications { get; private set; }

        public Job CreateJob(string employerId, string jobTitle, string jobLocation)
        {
            return new Job { EmployerId = employerId, JobTitle = jobTitle, JobLocation = jobLocation };
        }


        public JobApplication AddJobApplicant(int jobId, string applicantId)
        {
            var jobApplication = new JobApplication { JobId = jobId, ApplicantId = applicantId };
            JobApplications.Add(jobApplication);
            return jobApplication;
        }

        public void UpdateJob(Job job)
        {
            JobTitle = job.JobTitle;
            JobDescription = job.JobDescription;
            JobLocation = job.JobLocation;
            JobRequirements = job.JobRequirements;
            AnnualSalary = job.AnnualSalary;
            JobStatus = job.JobStatus;
        }
    }
}
