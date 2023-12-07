using DataAccessLayer.Base;
using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryEvents.Events
{
    public class SoftDeletableEvent : IRepositoryEvent<ISoftDeletable>
    {
        public void Invoke<TRepo, T2>(TRepo trepo)
            where TRepo : GenericBaseRepository<T2>
            where T2 : class, ISoftDeletable
        {
            trepo.OnDelete += (entity,args)=> {
                entity.IsDeleted = true;
                args.PassTheAction = true;
            };
        }
    }
}
