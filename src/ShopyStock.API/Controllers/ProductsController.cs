using Microsoft.AspNetCore.Mvc;
using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Products;
using ShopyStock.Application.Interfaces;

namespace ShopyStock.API.Controllers;

[Route("api/products")]
public  class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ProductResponseDto>>> GetAll([FromQuery] RequestProduct request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetAll(request, cancellationToken);
        return Ok(products);    
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetById(id, cancellationToken);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        await _productService.AddEntity(createProductDto, cancellationToken);
        return Created(string.Empty, new { message = "Product creado exitosamente" });
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(int id,UpdateProductDto updateProductDto, CancellationToken cancellationToken)
    {
        await _productService.UpdateEntity(id,updateProductDto, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _productService.DeleteEntity(id, cancellationToken);
        return NoContent();
    }
}