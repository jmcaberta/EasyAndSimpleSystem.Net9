using System.ComponentModel.DataAnnotations;

namespace System.Entity.Storedepot;

public class Entrance
{
    private int _entranceId;
    private int _companyId;
    private int _userId;
    private string _invoiceNumber;
    private string _invoice;
    private DateTime _entranceDate;
    private decimal _taxes;
    private decimal _total;
    private string _status;
    private ICollection<EntranceDetails> _entranceDetails;
    //private Company _company;
    //private Username _user;

    public int EntranceId
    {
        get => _entranceId;
        set
        {
            if (value <= 0) throw new AggregateException("Entrance Id cannot be less than zero.");
            _entranceId = value;
        }
    }
    [Required]
    public int CompanyId
    {
        get => _companyId;
        set
        {
            if (value <= 0) throw new AggregateException("Company Id cannot be less than zero.");
            _companyId = value;
        }
    }

    [Required]
    public int UserId
    {
        get => _userId;
        set
        {
            if (value <= 0) throw new AggregateException("User Id cannot be less than zero.");
            _userId = value;
        }
    }

    [Required]
    public string InvoiceNumber
    {
        get => _invoiceNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new AggregateException("Invoice Number cannot be empty.");
            _invoiceNumber = value;
        }
    }

    [Required]
    public string Invoice
    {
        get => _invoice;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new AggregateException("Invoice cannot be empty.");
            _invoice = value;
        }
    }

    [Required]
    public DateTime EntranceDate
    {
        get => _entranceDate;
        set => _entranceDate = value.Date;
    }

    [Required]
    public decimal Taxes
    {
        get => _taxes;
        set => _taxes = value < 0 ? 0 : value;
    }

    [Required]
    public decimal Total
    {
        get => _total;
        set => _total = value < 0 ? 0 : value;
    }

    [Required]
    public string Status
    {
        get => _status;
        set => _status = value?.ToUpper();
    }

    public ICollection<EntranceDetails> EntranceDetails
    {
        get => _entranceDetails ??= new List<EntranceDetails>();
        set => _entranceDetails = value;
    }

    /*public Company Company
    {
        get => _company;
        set => _company = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Username User
    {
        get => _user;
        set => _user = value ?? throw new ArgumentNullException(nameof(value));
    }*/
}