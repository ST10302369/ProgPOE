using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgPOE.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }  // This will store the hashed password

        [Required]
        public string Role { get; set; }

        // Foreign key for Farmer (optional, as Employee users don't have a farmer)
        public int? FarmerId { get; set; }

        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Display(Name = "Last Login")]
        [DataType(DataType.DateTime)]
        public DateTime? LastLogin { get; set; }

        // Navigation property for the Farmer
        [ForeignKey("FarmerId")]
        public virtual Farmer? Farmer { get; set; }
    }

    // Define roles as constants
    public static class UserRoles
    {
        public const string Farmer = "Farmer";
        public const string Employee = "Employee";
    }
}