using WebAPPMVC.Base.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace WebAPPMVC.Base.Models.DTOs.ProductDTO
{
    public class ProductCreateDTOs
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Range(EntityConstantModel.MIN_PRICE, EntityConstantModel.MAX_PRICE)]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public IFormFile Photo { get; set; }
        public int CategoryID { get; set; }
    }
}