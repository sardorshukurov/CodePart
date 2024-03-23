using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.DTOs;
using Services.Products;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("all", Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _service.GetAll();

                if (products.Any())
                {
                    return Ok(products);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("byName", Name = "GetAllByName")]
        public async Task<IActionResult> GetAllByName(string name)
        {
            try
            {
                var products = await _service.GetByName(name);

                if (products.Any())
                {
                    return Ok(products);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var product = await _service.GetProductById(id);

                return Ok(product);
            }
            catch (NotFoundException e)
            {
                Log.Warning(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO product)
        {
            try
            {
                await _service.AddProduct(product);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(Guid id, ProductDTO product)
        {
            try
            {
                return Ok(await _service.EditProduct(id, product));
            }
            catch (NotFoundException e)
            {
                Log.Warning(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _service.DeleteProduct(id);
                return Ok();
            }
            catch (NotFoundException e)
            {
                Log.Warning(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}
