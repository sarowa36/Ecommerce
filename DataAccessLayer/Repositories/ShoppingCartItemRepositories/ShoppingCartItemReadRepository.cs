using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.ShoppingCartItemRepositories
{
    public class ShoppingCartItemReadRepository : AbstractGenericReadRepository<ShoppingCartItem>, IShoppingCartItemReadRepository
    {
        public ShoppingCartItemReadRepository(ADC db) : base(db)
        {
        }

        public decimal CartTotal(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
