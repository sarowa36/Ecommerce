using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.CategoryRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.CategoryRepositories
{
    public class CategoryReadRepository : AbstractGenericReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(ADC db) : base(db)
        {
        }
    }
}
