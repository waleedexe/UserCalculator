using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICalculator
    {
        void Calculate<T>(RemoteEntities.UserOperation userOperation) where T : struct;
    }
}
