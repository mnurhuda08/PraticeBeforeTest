using WebAPPMVC.Base.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPPMVC.Base.Models
{
    [Table("Products",Schema ="master")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName {  get; set; }
        [Range(EntityConstantModel.MIN_PRICE, EntityConstantModel.MAX_PRICE)]
        public decimal Price { get; set; }
        public int Stock {  get; set; }

        public string? Photo { get; set; }

        [Column("CategoryID")]
        public int CategoryID {  get; set; }

        //relasi one to many
        public virtual Category Category { get; set; }
    }
}
