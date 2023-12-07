using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Base
{
    public interface IRepositoryEvent<T>
    {
        void Invoke<TRepo, T2>(TRepo trepo) where TRepo : GenericBaseRepository<T2> where T2 : class, T;
    }
}
