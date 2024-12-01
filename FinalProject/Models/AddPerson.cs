using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models;

public class AddPerson
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "About is required")]
    [StringLength(500, ErrorMessage = "About cannot be longer than 500 characters")]
    public string About { get; set; }
    
    [Required(ErrorMessage = "Please select a degree")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid degree")]
    [Display(Name = "Degree")]
    public int DegreeId { get; set; }
    
    public List<int>? Hobbies { get; set; }
}