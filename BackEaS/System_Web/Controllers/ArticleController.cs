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

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArtId == id);
        }
    }
}
