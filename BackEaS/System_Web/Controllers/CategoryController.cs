using System.Data;
using System.Entity.Storedepot;
using System.Web.Models.Storedepot.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace System.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public CategoryController(DbContextSystem context)
        {
            _context = context;
        }
        // Get: api/Category/Listing
        [HttpGet("{action}")]
        public async Task <IEnumerable<CategoryViewModel>> Listing()
        {
            var category = await _context.Categories.ToListAsync();
            return category.Select(c => new CategoryViewModel
            {
                CategoryId = c.CatId,
                CategoryName = c.CatName,
                CategoryDescription = c.CatDescription,
                IsActive = c.IsActive,
            });
        }
        // Get: api/Categories/Show/1
        [HttpGet("{action}/{id}")]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            var category = await _context.Categories.FindAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }
            return Ok(new CategoryViewModel
            {
                CategoryId = category.CatId,
                CategoryName = category.CatName,
                CategoryDescription = category.CatDescription,
                IsActive = category.IsActive
            });
        }
        // Put: api/Categories/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.CategoryId < 0)
            {
                return BadRequest();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CatId == model.CategoryId);

            if (category == null)
            {
                return NotFound();
            }
            
            category.CatName = model.CategoryName;
            category.CatDescription = model.CategoryDescription;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Save Exeption
                return BadRequest();
            }
            return Ok();
        }
        
        // Post: api/Categories/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category = new Category
            {
                CatName = model.CategoryName,
                CatDescription = model.CategoryDescription,
                IsActive = true
            };
            
            _context.Categories.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        
        // Delete: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CatId == id);
        }
        
    }
}
