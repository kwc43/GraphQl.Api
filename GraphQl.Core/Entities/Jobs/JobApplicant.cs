using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Users;

namespace GraphQl.Core.Entities.Jobs
{
    public class JobApplicant : EntityBase
    {
        public int JobId { get; set; }
        public string ApplicantId { get; set; }
    }
}
