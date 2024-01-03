using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.ShoppingCartItemRepositories
{
    public class ShoppingCartItemWriteRepository : AbstractGenericWriteRepository<ShoppingCartItem>, IShoppingCartItemWriteRepository
    {
        public ShoppingCartItemWriteRepository(ADC db) : base(db)
        {
        }
    }
}
