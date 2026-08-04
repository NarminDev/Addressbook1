using System.ComponentModel.DataAnnotations;

namespace Addressbook1.Areas.Admin.ViewModels.Category
{
    public record UpdateCategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name can not exceed 30 characters")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public string Name { get; set; }
    }
}
