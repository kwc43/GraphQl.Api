using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Users;

namespace GraphQl.Core.Entities.Jobs
{
    public class JobApplication : EntityBase
    {
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
    }
}
