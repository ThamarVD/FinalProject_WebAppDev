namespace FinalProject.Models;

public class Person
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string About { get; set; }
    
    public Degree Degree { get; set; }
    
    public List<Hobby> Hobbies { get; set; }
}