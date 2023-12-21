using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories
{
    public interface IShoppingCartItemReadRepository:IReadRepository<ShoppingCartItem>
    {
        decimal CartTotal(int userId);
    }
}
