using Microsoft.AspNetCore.Mvc;
using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;
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
    public async Task<ActionResult<PagedResult<ProductResponseDTO>>> GetAll([FromQuery] RequestProduct request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetAll(request, cancellationToken);
        
        return Ok(products);    
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDTO>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetById(id, cancellationToken);
        
        return Ok(product);
    }

}