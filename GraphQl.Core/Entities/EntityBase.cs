using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQl.Core.Entities
{
    public class EntityBase
    {
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
