using System.ComponentModel.DataAnnotations;

namespace WebAPPMVC.Base.Models.DTOs.CategoryDTO
{
    public class CategoryDTOs
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Category Name Max Length is 50")]
        public string? CategoryName { get; set; }

        public string? Description { get; set; }
        public string? Photo { get; set; }

    }
}