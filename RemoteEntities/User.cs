using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEntities
{
    public class User: RemoteBase
    {
        public string UserName { get; set; }
        public string LoginId { get; set; }
    }
}
