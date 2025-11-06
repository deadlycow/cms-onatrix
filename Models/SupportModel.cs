using System.ComponentModel.DataAnnotations;

namespace Onatrix.Models
{
  public class SupportModel
  {
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
    [Required]
    public string? Email { get; set; }
  }
}
