using Application.Core;
using Application.Extensions;

namespace Application.Models;
public class Person
{
    private readonly Name username;

    public Person(Name username) {
        this.username = username;
    }

    public string Username => username;
}