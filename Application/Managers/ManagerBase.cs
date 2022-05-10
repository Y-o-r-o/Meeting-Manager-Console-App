using System.Collections.Generic;
using Application.Core;
using Application.Helpers;
using Application.Meetings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Managers;

public class ManagerBase
{
    protected IMediator Mediator { get; }

    public ManagerBase(IServiceProvider services)
    {
        Mediator = services.GetRequiredService<IMediator>();
    }

    public Result Handle(IRequest<Result> request)
    {
        var result = Mediator.Send<Result>(request).Result;
        if (!result.IsSuccess)
        {
            Console.WriteLine(result.Error);
            Console.WriteLine("Try again? y/n");
            char key = BetterConsole.ReadKey();

            if (key.Equals('y'))
                return Handle(request);
            return Result.Failure("Operation canceled.");
        }
        return result;
    }

}