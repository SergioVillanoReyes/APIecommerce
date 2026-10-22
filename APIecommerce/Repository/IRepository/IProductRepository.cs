using System;
using APIecommerce.Models;

namespace APIecommerce.Repository.IRepository;

public interface IProductRepository
{

    ICollection<Product> GetProducts();

    ICollection<Product> GetProductsInPages(int pageNumber, int pageSize);

    int GetTotalProducts();

    ICollection<Product> GetProductsForCategory(int categoryId);

    ICollection<Product> SearchProducts(string searchTerm);
    
    Product? GetProduct(int id);

    bool BuyProduct(string name, int quantity);

    bool ProductExists(string name);

    bool ProductExists(int id);
    
    bool CreateProduct(Product product);

    bool UpdateProduct(Product product);

    bool DeleteProduct(Product product);

    bool Save();

}
