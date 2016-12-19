using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEntities
{
    public class RemoteBase
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }

        public RemoteBase()
        {
            Messages = new List<Message>();
        }
    }
}
