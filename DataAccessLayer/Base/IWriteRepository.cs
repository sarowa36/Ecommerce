using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Base
{
    public interface IWriteRepository<T>:IRepository<T> where T : class
    {
        Task CreateAsync(T t);
        Task CreateRangeAsync(IEnumerable<T> values);
        Task UpdateAsync(T t);
        Task UpdateRangeAsync(IEnumerable<T> values);
        Task DeleteAsync(T t);
        Task DeleteWhereAsync(Expression<Func<T,bool>> expression);
        Task DeleteAsync(object id);
        Task DeleteRangeAsync(IEnumerable<T> values);
        Task SaveChangesAsync();

    }
}
