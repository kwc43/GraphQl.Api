using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQl.Core.Entities.Users
{
    public class Employer : UserBase
    {
        internal Employer(string id, string fullName) : base(id, fullName)
        {
        }
    }
}
