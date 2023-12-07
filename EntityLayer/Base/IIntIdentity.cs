using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Base
{
    public interface IIntIdentity
    {
        /// <summary>
        /// Entity int primary key
        /// </summary>
        public int Id { get; set; }
    }
}
