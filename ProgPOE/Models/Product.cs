using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgPOE.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Production Date")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Display(Name = "Quantity")]
        public double Quantity { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; } // e.g., kg, liters, items

        [Display(Name = "Price per Unit")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerUnit { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        // Foreign key for Farmer - removing the Required attribute
        [Display(Name = "Farmer")]
        public int FarmerId { get; set; }

        // Navigation property for the Farmer
        [ForeignKey("FarmerId")]
        public virtual Farmer? Farmer { get; set; }
    }
}