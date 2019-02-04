using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Products.Models;


namespace Stateful1
{
    class ProductRepository : IProduct
    {
        private readonly IReliableStateManager stateManager;
        public ProductRepository(IReliableStateManager _stateManager)
        {
            stateManager = _stateManager;
        }
        public async Task AddAProduct(Product _product)
        {
            var products = await stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("myproducts");
            using (var tx = stateManager.CreateTransaction()) {
                await products.AddOrUpdateAsync(tx, _product.ProductID, _product, (id, value) => _product);
                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("myproducts");
            var mylistofProducts = new List<Product>();
            using (var tx = stateManager.CreateTransaction())
            {
                var myenumerable = await products.CreateEnumerableAsync(tx, EnumerationMode.Ordered);
                using (var myenumerator = myenumerable.GetAsyncEnumerator()) {
                    while (await myenumerator.MoveNextAsync(CancellationToken.None)){
                        KeyValuePair<Guid, Product> myvalue = myenumerator.Current;
                        mylistofProducts.Add(myvalue.Value);

                    }
                }


                    await tx.CommitAsync();
            }
            return mylistofProducts;
        }
    }
}
