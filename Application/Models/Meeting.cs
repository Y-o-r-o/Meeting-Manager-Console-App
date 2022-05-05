using Application.Core;
using Application.Extensions;
using Application.Helpers;

namespace Application.Models;
public class Meeting
{
    public Name? Name { get; set; }
    public Person? ResponsiblePerson { get; set; }
    public Description? Description { get; set; }
    public Category? Category { get; set; }
    public Type? Type { get; set; }
    public FromToDateTime? FromToDateTime { get; set; }
    public List<Person> Attendees { get; set; } = new List<Person>();
    
}