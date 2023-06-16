using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models;

public class Dish
{
    [Key]
    public int DishID {get; set;}
    [Required]
    [Display (Name="Chef's Name")]
    public string Chef { get; set;}
    [Required]
    public string Name { get; set;}
    [Required]
    [Range(1, 5000)]
    public int Calories { get; set;}
    [Required]
    [Range(1, 5)]
    public int Tastiness { get; set;}
    [Required]
    public string Description { get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}