using System.ComponentModel.DataAnnotations;

namespace System.Web.Models.Storedepot.Article;

public class CreateViewModel
{
    private int _catId;
    private string _artCode;
    private string _artName;
    private decimal _sellPrice;
    private int _itemCount;
    private string _artDescription;
    private bool _isActive;
    
    [Required]
    public int CatId
    {
        get => _catId;
        set => _catId = value;
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