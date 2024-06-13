using System.ComponentModel.DataAnnotations;

namespace CodingEvents.ViewModels;

public class AddEventCategoryViewModel
{
    [Required(ErrorMessage = "Category is required.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Must be between 3 and 20 characters.")]
    
    public string? Name { get; set; }

}
