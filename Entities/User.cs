using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User: EntityBase
    {
        public string UserName { get; set; }
        public string LoginId { get; set; }

        public ICollection<UserOperation> UserOperations { get; set; }
    }
}
