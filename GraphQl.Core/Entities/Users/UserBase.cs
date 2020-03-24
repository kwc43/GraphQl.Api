using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQl.Core.Entities.Users
{
    public class UserBase : EntityBase
    {
        public string Id { get; private set; }
        public string FullName { get; private set; }

        internal UserBase(string id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
    }
}
