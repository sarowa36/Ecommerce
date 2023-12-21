using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
