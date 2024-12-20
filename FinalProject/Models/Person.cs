using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FinalProject.Models;

public class Person
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "About is required")]
    [StringLength(500, ErrorMessage = "About cannot be longer than 500 characters")]
    public string About { get; set; }
    
    [Required(ErrorMessage = "Please select a degree")]
    public Degree Degree { get; set; }
    
    public List<Hobby>? Hobbies { get; set; } = new List<Hobby>();
}