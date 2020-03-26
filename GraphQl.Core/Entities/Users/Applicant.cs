using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Jobs;

namespace GraphQl.Core.Entities.Users
{
    public class Applicant : UserBase
    {
        public ICollection<JobApplication> JobApplications { get; private set; }

        internal Applicant(string id, string fullName) : base(id, fullName)
        {
        }
    }
}
