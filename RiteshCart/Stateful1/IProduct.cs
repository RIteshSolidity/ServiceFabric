using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Products.Models;

namespace Stateful1
{
    public interface IProduct
    {
        Task AddAProduct(Product _product);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
