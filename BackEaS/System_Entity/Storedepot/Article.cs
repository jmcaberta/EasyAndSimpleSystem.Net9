using System.ComponentModel.DataAnnotations;

namespace System.Entity.Storedepot;

public class Article
{
    private int _artId;
    private int _catId;
    private string _artCode;
    private string _artName;
    private decimal _sellPrice;
    private int _itemCount;
    private string _artDescription;
    private bool _isActive;
    
    public Category Category { get; set; }

    [Key]
    public int ArtId
    {
        get => _artId;
        private set
        {
            if (value <= 0) throw new AggregateException("The Article Id must be greater than zero"); _artId = value;
        }
    }
    [Required]
    public int CatId
    {
        get => _catId;
        private set
        {
            if (value <= 0) throw new AggregateException("The Category Id must be greater than zero"); _catId = value;
        }
    }
    [Required]
    public string ArtCode
    {
        get => _artCode;
        private set
        {
            if ( string.IsNullOrWhiteSpace(value)) throw new AggregateException("The Article Code cannot be empty"); _artCode = value;
        }
    }
    [Required]
    public string ArtName
    {
        get => _artName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new AggregateException("The Article Name cannot be empty"); _artName = value;
        }
    }
    [Required]
    public decimal SellPrice
    {
        get => _sellPrice;
        private set
        {
            if (value <= 0) throw new ArgumentException("The Sell Price must be greater than zero"); _sellPrice = value;
        }
    }
    [Required]
    public int ItemCount
    {
        get => _itemCount;
        private set
        {
            if (value <= 0) throw new AggregateException("The Item Count must be greater than zero"); _itemCount = value;
        }
    }
    [Required]
    public string ArtDescription
    {
        get => _artDescription;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new AggregateException("The Article Description cannot be empty"); _artDescription = value;
        }
    }
    [Required]
    public bool IsActive
    {
        get => _isActive;
        private set => _isActive = value;
    }
}