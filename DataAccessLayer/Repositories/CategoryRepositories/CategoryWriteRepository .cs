using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.CategoryRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.CategoryRepositories
{
    public class CategoryWriteRepository : AbstractGenericWriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(ADC db) : base(db)
        {
        }
    }
}
