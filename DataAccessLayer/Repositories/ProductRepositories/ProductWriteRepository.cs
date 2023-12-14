using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.ProductRepositories
{
    public class ProductWriteRepository : AbstractGenericWriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ADC db) : base(db)
        {
        }
    }
}
