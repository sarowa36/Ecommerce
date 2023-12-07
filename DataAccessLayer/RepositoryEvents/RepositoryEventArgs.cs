using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryEvents
{
    public class RepositoryEventArgs
    {
        /// <summary>
        /// If you want to cancel repository method, make this <see langword="true"/>
        /// </summary>
        public bool IsCancelled { get; set; } = false;
        /// <summary>
        /// If you doesnt want to repository method changes, make this <see langword="true"/>
        /// </summary>
        public bool PassTheAction { get; set; } = false;
    }
}
