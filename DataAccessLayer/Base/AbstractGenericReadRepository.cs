using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Base
{
    public abstract class AbstractGenericReadRepository<T> : IReadRepository<T> where T : class
    {
        public DbSet<T> Table => _db.Set<T>();
        private readonly ADC _db;
        public AbstractGenericReadRepository(ADC db)
        {
            _db = db;
        }
        public virtual T? Get(object id)
        {
            return Table.Find(id);
        }
        public virtual bool Get(object id, out T? t)
        {
            t = this.Get(id);
            return t != null;
        }
        public virtual T? GetWhere(Expression<Func<T, bool>> expression)
        {
            return Table.FirstOrDefault(expression);
        }
        public bool GetWhere(Expression<Func<T, bool>> expression, out T? t)
        {
            t = this.GetWhere(expression);
            return t != null;
        }
        public virtual IQueryable<T> GetAll(int? index = null, int? count = null, string? orderPropertyName = null, bool isDesc = false)
        {
            var query = Table.AsQueryable();
            if(orderPropertyName != null && isDesc)
                query=query.OrderByDescending(x=>EF.Property<object>(x, orderPropertyName));
            else if(orderPropertyName!=null)
                query = query.OrderBy(x => EF.Property<object>(x, orderPropertyName));
            if (index != null)
                query = query.Skip((int)index);
            if (count != null)
                query = query.Take((int)count);
            return query;
        }
        public virtual bool Exist(Expression<Func<T, bool>> expression)
        {
            return Table.Any(expression);
        }
        public virtual int Count(Expression<Func<T, bool>>? expression = null)
        {
            if(expression!=null)
                return Table.Count(expression);
            return Table.Count();
        }
    }
}
