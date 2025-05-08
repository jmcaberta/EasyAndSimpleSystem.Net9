using System.Data;
using System.Entity.Storedepot;
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

        public async Task<ActionResult<IEnumerable<ArticleViewModel>>> Listing()
        {
            try
            {
                var article = await _context.Articles.Include(a => a.Category).ToListAsync();
                return Ok(article.Select(a => new ArticleViewModel
                {
                    ArticleId = a.ArticleId,
                    CatId = a.CatId,
                    CategoryName = a.Category.CatName,
                    ArtCode = a.ArtCode,
                    ArtName = a.ArtName,
                    SellPrice = a.SellPrice,
                    ItemCount = a.ItemCount,
                    ArtDescription = a.ArtDescription,
                    IsActive = a.IsActive,
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in Listing: {e.Message}");
                Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Listing");
            }
            
        }
        
        // Get: api/Article/Show/1

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            var article = await _context.Articles.Include(a => a.Category).SingleOrDefaultAsync(a => a.ArticleId == id);

            if (article == null)
            {
                return NotFound();
            }
            return Ok(new ArticleViewModel
            {
                ArticleId = article.ArticleId,
                CatId = article.CatId,
                CategoryName = article.Category.CatName,
                ArtCode = article.ArtCode,
                ArtName = article.ArtName,
                SellPrice = article.SellPrice,
                ItemCount = article.ItemCount,
                ArtDescription = article.ArtDescription,
                IsActive = article.IsActive
            });
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
            
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == model.ArticleId);
            
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
        
        // Post: api/Article/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Article article = new Article
            {
                CatId = model.CatId,
                ArtCode = model.ArtCode,
                ArtName = model.ArtName,
                SellPrice = model.SellPrice,
                ItemCount = model.ItemCount,
                ArtDescription = model.ArtDescription,
                IsActive = true
            };
            return Ok();
        }
        
        //Put: appi/Article/SetActivateStatus/{id}?isActive=true
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> SetActivateStatus([FromRoute] int id, [FromQuery] bool isActive)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == id);

            if (article == null)
            {
                return NotFound();
            }
            
            article.IsActive = isActive;

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

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
