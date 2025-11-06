using System.ComponentModel.DataAnnotations;

namespace Onatrix.Models;
public class QuestionModel
{
  [Required(ErrorMessage = "Name is required.")]
  [MinLength(2, ErrorMessage = "At least 2 characters.")]
  public string? Name { get; set; }

  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
  [Required]
  public string? QEmail { get; set; }

  [Required(ErrorMessage ="A question must be asked.")]
  [MinLength(10, ErrorMessage = "Question must be at least 10 characters long.")]
  public string Question { get; set; } = null!;
}
