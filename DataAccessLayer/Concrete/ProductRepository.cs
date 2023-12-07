using DataAccessLayer.Base;
using DataAccessLayer.RepositoryEvents;
using EntityLayer.Base;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ProductRepository : GenericBaseRepository<Product>
    {
        public ProductRepository(IServiceProvider serviceProvider,ADC db) : base(serviceProvider,db)
        {
        }
    }
}
