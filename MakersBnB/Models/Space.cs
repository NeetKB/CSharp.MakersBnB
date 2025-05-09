namespace MakersBnB.Models;
using System.ComponentModel.DataAnnotations;

public class Space
{

  [Key]
  public int Id {get; set;}
  // the ? indicates that a field can be null / blank (is nullable)
  public string? Name {get; set;}
  public string? Description {get; set;}
  public int? Price {get; set;}
  public int? Bedrooms {get; set;}
  public string? Rules {get;set;}

  // the constructor
  public Space(string name, string description, int price, int bedrooms, string rules) {
    this.Name = name;
    this.Description = description;
    this.Price = price;
    this.Bedrooms =bedrooms;
    this.Rules = rules;
  }

  public Space(){}
}
