using System.Data;
using System.Entity.Storedepot;
using System.Web.Models.Storedepot.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.CategoryId <= 0)
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
        
        // Delete: api/Category/Delete/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error deleting the category. Check dependencies.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
            
            return NoContent();
        }
        
        // PUT /api/Categories/SetActivateStatus/{id}?isActive=true
        [HttpPut("SetActivateStatus/{id}")]
        public async Task<ActionResult> SetActivateStatus([FromRoute] int id, [FromQuery] bool isActive)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CatId == id);

            if (category == null)
            {
                return NotFound();
            }
            
            category.IsActive = isActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CatId == id);
        }
        
    }
}
