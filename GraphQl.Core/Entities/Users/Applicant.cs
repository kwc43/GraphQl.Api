using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQl.Core.Entities.Users
{
    public class Applicant : UserBase
    {
        internal Applicant(string id, string fullName) : base(id, fullName)
        {
        }
    }
}
