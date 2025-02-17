namespace System.Entity.Storedepot;

public class EntranceDetails
{
    private int _entDetId;
    private int _entId;
    private int _artId;
    private int _itemCount;
    private decimal _price;
    private Entrance _entrance;
    private Article _article;

    public int EntranceDetId
    {
        get => _entDetId;
        set
        {
            if (value <=0) throw new ArgumentException("EntranceDetId cannot be less than zero."); _entDetId = value;
        }
    }

    public int EntranceId
    {
        get => _entId;
        set
        {
            if (value <=0) throw new ArgumentException("EntranceId cannot be less than zero."); _entId = value;
        }
    }

    public int ArtId
    {
        get => _artId;
        set => _artId = value;
    }

    public int ItemCount
    {
        get => _itemCount;
        set
        {
            if (value < 0) throw new ArgumentException("ItemCount cannot be less than zero."); _itemCount = value;
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0) throw new ArgumentException("Price cannot be less than zero."); _price = value;
        }
    }

    public Entrance Entrance
    {
        get => _entrance;
        set => _entrance = value;
    }

    public Article Article
    {
        get => _article;
        set => _article = value;
    }
}