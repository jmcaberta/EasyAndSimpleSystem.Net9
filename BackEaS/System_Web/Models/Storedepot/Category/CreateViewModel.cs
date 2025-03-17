using System.ComponentModel.DataAnnotations;

namespace System.Web.Models.Storedepot.Category;

public class CreateViewModel
{
    private string _categoryName;
    private string _categoryDescription;
    private bool _isActive;
    
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 50 characters")]
    public string CategoryName
    {
        get => _categoryName; 
        set => _categoryName = value;
    }
    [StringLength(256)]
    public string CategoryDescription
    {
        get => _categoryDescription; 
        set => _categoryDescription = value;
    }

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }
}