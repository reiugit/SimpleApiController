using Microsoft.AspNetCore.Mvc;
using SimpleApiController.Entities;
using SimpleApiController.Services;

namespace SimpleApiController.Controllers;

[Route("[controller]")]
public class ProductsController(ProductsService productsService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ProductResponse>> GetAll()
    {
        return productsService
            .GetAllProducts()
            .Select(ProductResponse.FromProduct)
            .ToList();
    }

    [HttpGet("{productid:int}")]
    public ActionResult<ProductResponse> Get(int productId)
    {
        var product = productsService.GetProduct(productId);

        return product is null
            ? Problem(detail: $"Product {productId} not found.", statusCode: StatusCodes.Status404NotFound)
            : ProductResponse.FromProduct(product);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateProductRequest createProductRequest)
    {
        var product = createProductRequest.ToProduct();

        productsService.CreateProduct(product);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { productid = product.Id },
            value: ProductResponse.FromProduct(product));
    }
}

public record CreateProductRequest(string Name = "")
{
    public Product ToProduct()
        => new() { Name = Name };
}

public record ProductResponse(int Id, string Name = "")
{
    public static ProductResponse FromProduct(Product product)
        => new(product.Id, product.Name);
}


