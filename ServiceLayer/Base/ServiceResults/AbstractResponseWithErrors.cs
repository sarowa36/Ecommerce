using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Base.ServiceResults
{
    public abstract class AbstractResponseWithErrors
    {
        public bool IsSuccess { get { return Errors.Count == 0; } }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public virtual void BindResponse(AbstractResponseWithErrors response)
        {
            if (this.IsSuccess)
                this.Errors = response.Errors;
        }
    }
}
