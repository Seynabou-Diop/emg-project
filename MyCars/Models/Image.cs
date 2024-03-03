using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCars.Models;

public class Image
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set; }

    [NotMapped]
    [Display(Name = "Image")]
    public IFormFile File { get; set; }

    public int CarId { get; set; }
    public Car? Car { get; set; }
}
