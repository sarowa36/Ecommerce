using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.ProductRepositories
{
    public class ProductWriteRepository : AbstractGenericWriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ADC db) : base(db)
        {
        }
    }
}
