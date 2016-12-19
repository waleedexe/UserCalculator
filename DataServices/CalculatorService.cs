using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteEntities;

namespace Services
{
    public class CalculatorService: ICalculator 
    {
        public void Calculate<T>(UserOperation userOperation) where T: struct
        {
            dynamic n1 = (T)Convert.ChangeType(userOperation.FirstNumber, typeof(T));
            dynamic n2 = (T)Convert.ChangeType(userOperation.SecondNumber, typeof(T));
            dynamic result;

            var op = (Entities.OperationTypes)Convert.ToInt32(userOperation.Operation);

            switch (op)
            {
                case Entities.OperationTypes.Add:
                    result = n1 + n2;
                    break;
                case Entities.OperationTypes.Subtract:
                    result = n1 - n2;
                    break;
                case Entities.OperationTypes.Divide:
                    try
                    {
                        result = n1 / n2;
                    }
                    catch (DivideByZeroException)
                    {
                        result = "NaN";
                    }
                    break;
                default:
                    result = "";
                    break;
            }

            userOperation.Result = Convert.ToString(result);
        }
    }
}
