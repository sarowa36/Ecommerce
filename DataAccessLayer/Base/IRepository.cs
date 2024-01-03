using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Base
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
    }
}
