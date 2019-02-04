using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Products.Models;


namespace Web1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController()
        {
            _productService = ServiceProxy.Create<IProductService>(new Uri("fabric:/RiteshCart/Stateful1"), new ServicePartitionKey(0));
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            IEnumerable<Product> prods = await _productService.GetAllProducts();
            return Ok(prods);
        }

        // GET api/values/5
        [HttpPost]
        public async Task Post([FromBody] Product _product)
        {
            await _productService.AddAProduct(_product);
        }


    }
}
