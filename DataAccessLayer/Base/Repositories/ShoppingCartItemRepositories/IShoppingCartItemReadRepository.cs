using EntityLayer.Entities;

namespace DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories
{
    public interface IShoppingCartItemReadRepository:IReadRepository<ShoppingCartItem>
    {
        decimal CartTotal(int userId);
    }
}
