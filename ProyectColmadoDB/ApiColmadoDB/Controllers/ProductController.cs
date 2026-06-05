using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiColmadoDB;
using ApiColmadoDB.Dto;
using ApiColmadoDB.Entities;
using Microsoft.EntityFrameworkCore;
namespace ApiColmadoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDataContext _context;
        public ProductController(MyDataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductDto dto)
        {
            var newProduct = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                SalePrice = dto.SalePrice,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };
            _context.Add(newProduct);
            _context.SaveChanges();
            return Ok(newProduct);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var AllProducts = _context.Products.ToList();
            return Ok(AllProducts);
        }
        [HttpGet("Incluir categoria")]
        public IActionResult GetAllWhithCategory()
        {
            List<ProductCompleteDto> ListProducts = new List<ProductCompleteDto>();
            var productos = _context.Products.Include(p => p.Category).Select(p => new ProductCompleteDto
            {
                ProductName = p.ProductName,
                CategoryName = p.Category.CategoryName,
                Price = p.Price,
                SalePrice = p.SalePrice,
                Stock = p.Stock
            }).ToList();
            return Ok(productos);
        }
        [HttpGet("Filtar {ProductId}")]
        public IActionResult GetAllWhithCategoryProduct(int ProductId)
        {
            List<ProductCompleteDto> ListProducts = new List<ProductCompleteDto>();
            var productos = _context.Products.Include(p => p.Category).Where(t => t.IdProduct == ProductId).Select(p => new ProductCompleteDto
            {
                ProductName = p.ProductName,
                CategoryName = p.Category.CategoryName,
                Price = p.Price,
                SalePrice = p.SalePrice,
                Stock = p.Stock
            }).ToList();
            return Ok(productos);
        }

        [HttpPut]
        public IActionResult UpdateProduct(int id, ProductDto dto)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"El id {id} no existe en la base de datos");
            }
            product.ProductName = dto.ProductName;

            product.Price = dto.Price;
            product.SalePrice = dto.SalePrice;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;
            _context.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpDelete]
        public IActionResult SoftDelete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"El {id} no existe en la base de datos");
            }
            product.IsDelete = '1';
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok(product);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"El id {id} no existe en la base de datos");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet("Solo los que no estan borrado")]
        public IActionResult GetAllWithCondition()
        {
            var ListProducts = _context.Products.Where(p => p.IsDelete == '0');
            return Ok(ListProducts);
        }
        [HttpGet("Solo los que no estan borrado con la categoria")]
        public IActionResult GetAllWithConditionAndCategory()
        {
            List<ProductCompleteDto> ListProducts = new List<ProductCompleteDto>();
            var productos = _context.Products.Include(p => p.Category).Where(t => t.IsDelete == '0').Select(p => new ProductCompleteDto
            {
                ProductName = p.ProductName,
                CategoryName = p.Category.CategoryName,
                Price = p.Price,
                SalePrice = p.SalePrice,
                Stock = p.Stock
            }).ToList();
            return Ok(productos);
        }

    }
}
