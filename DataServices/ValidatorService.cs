using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteEntities;
using System.Text.RegularExpressions;

namespace Services
{
    public class DataValidationService : IDataValidator
    {
        public bool Validate(UserOperation userOperation)
        {
            if (userOperation == null)
                return false;

            if (!IsNumber(userOperation.FirstNumber))
                return false;
            if (!IsNumber(userOperation.SecondNumber))
                return false;
            if (!IsOperation(userOperation.Operation))
                return false;

            return true;
        }

        private bool IsNumber(string number)
        {
            var rgx = new Regex(@"^-?[0-9]\d*(\.\d+)?$", RegexOptions.Compiled);
            return rgx.IsMatch(number);
        }

        private bool IsOperation(string op)
        {
            var rgx = new Regex(@"^[1-3]$", RegexOptions.Compiled);
            return rgx.IsMatch(op);
        }
    }
}
