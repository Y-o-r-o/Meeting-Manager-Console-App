namespace Application.Models;
public class Person
{
    public Name username { get; }

    public Person(Name username) {
        this.username = username;
    }

    public string Username => username;
}