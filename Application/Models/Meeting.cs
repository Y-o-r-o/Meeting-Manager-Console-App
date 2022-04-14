using Application.Core;
using Application.Extensions;
using Application.Helpers;

namespace Application.Models;
public class Meeting
{
    const string DEFAULT_MEETING_NAME = "meeting";

    public Guid Id { get; set; }
    public Name Name { get; set; }
    public Person ResponsiblePerson { get; set; }
    public Description Description { get; set; } = new Description();
    public Category Category { get; set; } = new Category();
    public Type Type { get; set; } = new Type();
    public FromToDateTime FromToDateTime { get; set; } = new FromToDateTime();
    public List<Person> Attendees { get; set; } = new List<Person>();


    public Meeting(Person responsiblePerson)
    {
        if(responsiblePerson is null)
            throw new ArgumentException("Meeting must have responsible person.");

        ResponsiblePerson = responsiblePerson;
        Name = new Name(DEFAULT_MEETING_NAME + Id.ToString());
    }

}