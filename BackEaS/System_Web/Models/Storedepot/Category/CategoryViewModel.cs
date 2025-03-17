namespace System.Web.Models.Storedepot.Category;

public class CategoryViewModel
{
    private int _categoryId;
    private string _categoryName;
    private string _categoryDescription;
    private bool _isActive;
    
    public int CategoryId { get => _categoryId; set => _categoryId = value; }
    public string CategoryName { get => _categoryName; set => _categoryName = value; }
    public string CategoryDescription { get => _categoryDescription; set => _categoryDescription = value; }
    public bool IsActive { get => _isActive; set => _isActive = value; }
}