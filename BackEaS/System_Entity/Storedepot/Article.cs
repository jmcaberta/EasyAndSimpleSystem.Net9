using System.ComponentModel.DataAnnotations;

namespace System.Entity.Storedepot;

public class Article
{
    private int _articleId;
    private int _catId;
    public Category Category { get; set; }
    private string _artCode;
    private string _artName;
    private decimal _sellPrice;
    private int _itemCount;
    private string _artDescription;
    private bool _isActive;
    
    

    [Key]
    public int ArticleId
    {
        get => _articleId;
        set
        {
            if (value <= 0) throw new AggregateException("The Article Id must be greater than zero"); _articleId = value;
        }
    }
    [Required]
    public int CatId
    {
        get => _catId;
        set
        {
            if (value <= 0) throw new AggregateException("The Category Id must be greater than zero"); _catId = value;
        }
    }
    [Required]
    public string ArtCode
    {
        get => _artCode;
        set
        {
            if ( string.IsNullOrWhiteSpace(value)) throw new AggregateException("The Article Code cannot be empty"); _artCode = value;
        }
    }
    [Required]
    public string ArtName
    {
        get => _artName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new AggregateException("The Article Name cannot be empty"); _artName = value;
        }
    }
    [Required]
    public decimal SellPrice
    {
        get => _sellPrice;
        set
        {
            if (value <= 0) throw new ArgumentException("The Sell Price must be greater than zero"); _sellPrice = value;
        }
    }
    [Required]
    public int ItemCount
    {
        get => _itemCount;
        set
        {
            if (value <= 0) throw new AggregateException("The Item Count must be greater than zero"); _itemCount = value;
        }
    }
    [Required]
    public string ArtDescription
    {
        get => _artDescription;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new AggregateException("The Article Description cannot be empty"); _artDescription = value;
        }
    }
    [Required]
    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }
}