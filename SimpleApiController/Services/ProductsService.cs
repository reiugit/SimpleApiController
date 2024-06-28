using SimpleApiController.Entities;

namespace SimpleApiController.Services;

public class ProductsService
{
    private static List<Product> _productsRepository =
    [
        new() { Id = 1, Name = "Product 1" },
        new() { Id = 2, Name = "Product 2" },
    ];

    public List<Product> GetAllProducts() => _productsRepository;

    public Product? GetProduct(int productId) => _productsRepository.Find(p => p.Id == productId);

    public void CreateProduct(Product product)
    {
        product.Id = _productsRepository.Max(p => p.Id) + 1;
        _productsRepository.Add(product);
    }
}
