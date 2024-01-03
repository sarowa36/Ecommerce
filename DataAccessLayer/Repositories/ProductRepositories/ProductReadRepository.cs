using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.ProductRepositories
{
    public class ProductReadRepository : AbstractGenericReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(ADC db) : base(db)
        {
        }
    }
}
