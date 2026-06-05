using ApiColmadoDB.Dto;
using ApiColmadoDB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiColmadoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDataContext _context;
        public CategoryController(MyDataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryDto dto)
        {
            var newCategory = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return Ok(newCategory);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var CategoryList = _context.Categories.ToList();
            return Ok(CategoryList);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound($"El Id {id} no existe en la base de datos");
            }
            return Ok(category);
        }
        [HttpPut]
        public IActionResult UpdateCategory(int id, CategoryDto dto)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound($"El id {id} no existe en la base de datos");
            }
            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound($"El id {id} no existe en la base de datos");
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult SoftDelelete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound($"El id {id} no exite en la base de datos");
            }
            category.Isdelete = '1';
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Ok(category);
        }
        [HttpGet("Solo lo que no estan borrados")]
        public IActionResult GetAllWithCondition()
        {
            var CategoryList = _context.Categories.Where(c => c.Isdelete == '0');
            return Ok(CategoryList);
        }

    }
}
