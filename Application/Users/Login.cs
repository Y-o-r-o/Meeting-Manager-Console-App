using Application.Core;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Users;

public class Login
{
    public class Command : IRequest<Result<Person>> { }
    public class Handler : IRequestHandler<Command, Result<Person>>
    {
        public Task<Result<Person>> Handle(Command request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Enter your username:");
            var usernameStr = BetterConsole.ReadLineAndClear();
            Person currentUser;
            try
            {
                Name username = new Name(usernameStr);
                currentUser = new Person(username);
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result<Person>.Failure(ex.Message));
            }

            BetterConsole.WaitForKeypress("Logged in.");
            return Task.FromResult(Result<Person>.Success(currentUser));
        }

    }
}