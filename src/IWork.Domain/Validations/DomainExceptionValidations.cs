using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Validations
{
    public class DomainExceptionValidations : Exception
    {
        public DomainExceptionValidations(string error) : base(error) { }
        public static void ExceptionHandler(bool hasError, string error)
        {
            if (hasError)
            {
                throw new DomainExceptionValidations(error);
            }
        }
    }
}
