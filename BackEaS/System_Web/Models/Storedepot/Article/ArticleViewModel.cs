namespace System.Web.Models.Storedepot.Article;

public class ArticleViewModel
{
    private int _articleId;
    private int _catId;
    private string _artCode;
    private string _artName;
    private decimal _sellPrice;
    private int _itemCount;
    private string _artDescription;
    private bool _isActive;
    
    public int ArticleId { get => _articleId; set => _articleId = value; }
    public int CatId { get => _catId; set => _catId = value; }
    public string CategoryName { get; set; }
    public string ArtCode { get => _artCode; set => _artCode = value; }
    public string ArtName { get => _artName; set => _artName = value; }
    public decimal SellPrice { get => _sellPrice; set => _sellPrice = value; }
    public int ItemCount { get => _itemCount; set => _itemCount = value; }
    public string ArtDescription { get => _artDescription; set => _artDescription = value; }
    public bool IsActive { get => _isActive; set => _isActive = value; }
   
    
}