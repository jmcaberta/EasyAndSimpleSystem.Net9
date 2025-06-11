using System.ComponentModel.DataAnnotations;

namespace System.Web.Models.Storedepot.Article;

public class UpdateViewModel
{
    private int _articleId;
    private int _catId;
    private string _artCode;
    private string _artName;
    private decimal _sellPrice;
    private int _itemCount;
    private string _artDescription;
    private bool _isActive;
    
    [Required]
    public int ArticleId { get => _articleId; set => _articleId = value; }
    [Required]
    public int CatId { get => _catId;  set => _catId = value; }
    [Required]
    public string ArtCode { get => _artCode; set => _artCode = value; }
    
    [Required]
    public string ArtName { get => _artName; set => _artName = value; }
    [Required]
    public decimal SellPrice { get => _sellPrice; set => _sellPrice = value; }
    [Required]
    public int ItemCount { get => _itemCount; set => _itemCount = value; }
    [Required]
    public string ArtDescription { get => _artDescription; set => _artDescription = value; }
    public bool IsActive { get => _isActive; set => _isActive = value; }
    
}