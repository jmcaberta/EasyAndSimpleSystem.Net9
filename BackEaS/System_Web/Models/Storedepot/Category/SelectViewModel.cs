namespace System.Web.Models.Storedepot.Category;

public class SelectViewModel
{
    private int _categoryId;
    private string _categoryName;
    
    public int CatId { get => _categoryId; set => _categoryId = value; }
    public string CategoryName { get => _categoryName; set => _categoryName = value; }
}