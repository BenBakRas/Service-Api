using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public interface IProduct
    {
        Product GetProductById(int id);
        List<Product> GetAllProducts();
        int CreateProduct(Product aProduct);
        bool DeleteProductById(int id);
        bool UpdateProductById(Product productToUpdate);
    }
}
