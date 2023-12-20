using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Base
{
    public abstract class AbstractGenericWriteRepository<T> : IWriteRepository<T> where T : class
    {
        public DbSet<T> Table { get { return _db.Set<T>(); } }
        private readonly ADC _db;
        public AbstractGenericWriteRepository(ADC db)
        {
            _db = db;
        }

        public virtual async Task CreateAsync(T t)
        {
           await Table.AddAsync(t);
        }
        public virtual async Task CreateRangeAsync(IEnumerable<T> values)
        {
           await Table.AddRangeAsync(values);
        }
        public virtual async Task UpdateAsync(T t)
        {
            Table.Update(t);
        }
        public virtual async Task UpdateRangeAsync(IEnumerable<T> values)
        {
            Table.UpdateRange(values);
        }
        public virtual async Task DeleteAsync(T t)
        {
            Table.Remove(t);
        }
        public virtual async Task DeleteAsync(object id)
        {
            var val = Table.Find(id);
            if (val == null)
                throw new Exception("Object not found");
            await DeleteAsync(val);
        }
        public virtual async Task DeleteWhereAsync(Expression<Func<T, bool>> expression)
        {
            var val = Table.FirstOrDefault(expression);
            if (val == null)
                throw new Exception("Object not found");
            await DeleteAsync(val);
        }
        public virtual async Task DeleteRangeAsync(IEnumerable<T> values)
        {
            Table.RemoveRange(values);
        }
        public virtual async Task<int> SaveChangesAsync()
        {
           return await _db.SaveChangesAsync();
        }
    }
}
