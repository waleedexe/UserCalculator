using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserOperation: EntityBase
    {
        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }
        public string Operation { get; set; }
        public string Result { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
