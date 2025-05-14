using System.ComponentModel.DataAnnotations;

namespace ProgPOE.Models
{
    public class Farmer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        [Display(Name = "Farm Location")]
        public string FarmLocation { get; set; }

        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Navigation property for products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}