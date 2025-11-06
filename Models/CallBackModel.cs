using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Onatrix.Models;
public class CallBackModel
{
  [Required(ErrorMessage = "Name is required.")]
  [MinLength(2, ErrorMessage = "At least 2 characters.")]
  public string Name { get; set; } = null!;

  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
  [Required]
  public string? Email { get; set; }

  [Required(ErrorMessage = "Phone number is required.")]
  [RegularExpression(@"^\+?[0-9\s\-()]{7,20}$", ErrorMessage = "Please enter a valid phone number.")]
  public string? PhoneNumber { get; set; }

  [Required(ErrorMessage = "Please select an option.")]
  public string Option { get; set; } = null!;
}
