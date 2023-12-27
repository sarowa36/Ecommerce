using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Base.ServiceResults
{
    public abstract class AbstractResponseWithErrorsAndValue<T>:AbstractResponseWithErrors
    {
        public T Value { get; set; }
    }
}
