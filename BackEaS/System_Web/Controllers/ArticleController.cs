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

        public async Task<IEnumerable<ArticleViewModel>> Listing()
        {
            var article = await _context.Articles.ToListAsync();
            return article.Select(a => new ArticleViewModel
            {
                ArticleId = a.ArtId,
                CatId = a.CatId,
                ArtCode = a.ArtCode,
                ArtName = a.ArtName,
                SellPrice = a.SellPrice,
                ItemCount = a.ItemCount,
                ArtDescription = a.ArtDescription,
                IsActive = a.IsActive,
                
            });
        }
    }
}
