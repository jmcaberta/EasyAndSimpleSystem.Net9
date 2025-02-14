using System.ComponentModel.DataAnnotations;

namespace System.Entity.Storedepot;

public class Category
{
    private int _catId;
    private string _catName;
    private string _catDescription;
    private bool _isActive;
    private ICollection<Article> _articles;

    public int CatId
    {
        get => _catId; set { if (value <= 0) throw new ArgumentException("Category id must be greater than zero."); _catId = value; }
    }
    [Required]
    public string CatName
    {
        get => _catName; set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Category name cannot be empty."); _catName = value; }
    }
    [StringLength(256)]
    public string CatDescription
    {
        get => _catDescription;
        set => _catDescription = value;
    }

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    public ICollection<Article> Articles
    {
        get => _articles ??= new List<Article>();
        set => _articles = value;
    }
}