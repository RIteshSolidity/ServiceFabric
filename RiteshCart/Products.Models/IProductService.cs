using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Models
{
    public interface IProductService : IService
    {
        Task AddAProduct(Product _product);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
