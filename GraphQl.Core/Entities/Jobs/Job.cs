using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Users;
using GraphQl.Core.Values;

namespace GraphQl.Core.Entities.Jobs
{
    public class Job : EntityBase
    {
        public string Id { get; private set; }
        public int EmployerId { get; private set; }
        public string JobTitle { get; private set; }
        public string JobDescription { get; private set; }
        public string JobLocation { get; private set; }
        public string JobRequirements { get; private set; }
        public int? AnnualSalary { get; private set; }
        public JobStatus JobStatus { get; private set; }
        public List<JobApplicant> JobApplicants { get; private set; }

        public Job CreateJob(int employerId, string jobTitle, string jobLocation)
        {
            return new Job { EmployerId = employerId, JobTitle = jobTitle, JobLocation = jobLocation};
        }


        public JobApplicant AddJobApplicant(int jobId, string applicantId)
        {
            var jobApplicant = new JobApplicant { JobId = jobId, ApplicantId = applicantId};
            JobApplicants.Add(jobApplicant);
            return jobApplicant;
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
