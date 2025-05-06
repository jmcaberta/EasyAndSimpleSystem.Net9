using System.Data;
using System.Web.Models.Storedepot.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace System.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public ArticleController(DbContextSystem context)
        {
            _context = context;
        }
        // Get: api/Article/Listing
        [HttpGet("{action}")]

        public async Task<IActionResult> Listing()
        {
            try
            {
                var articles = await _context.Articles.Include(a => a.Category)
                    .Select(a => new ArticleViewModel
                    {
                        ArticleId = a.ArtId,
                        CatId = a.CatId,
                        CategoryName = a.Category.CatName,
                        ArtCode = a.ArtCode,
                        ArtName = a.ArtName,
                        SellPrice = a.SellPrice,
                        ItemCount = a.ItemCount,
                        ArtDescription = a.ArtDescription,
                        IsActive = a.IsActive,
                    }).ToListAsync();
                return Ok(articles);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in Listing: {e.Message}");
                Console.WriteLine(e.ToString());
                return StatusCode(500, new { message = "Internal Server Error" });
            }
            
        }
        
        // Put: api/Article/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.ArticleId <= 0)
            {
                return BadRequest();
            }
            
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.ArtId == model.ArticleId);
            
            if (article == null)
            {
                return BadRequest();
            }
            
            article.CatId = model.CatId;
            article.ArtCode = model.ArtCode;
            article.ArtName = model.ArtName;
            article.SellPrice = model.SellPrice;
            article.ItemCount = model.ItemCount;
            article.ArtDescription = model.ArtDescription;
            article.IsActive = model.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArtId == id);
        }
    }
}
