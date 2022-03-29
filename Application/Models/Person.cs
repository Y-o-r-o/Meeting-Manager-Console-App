using Application.Core;
using Application.Extensions;

namespace Application.Models;
public class Person
{
    public Name Username { get; set; } = new Name();

    public string getName(){
        return Username.Value;
    }

}