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
        Console.Clear();
        var result = Mediator.Send<Result>(request).Result;
        if (!result.IsSuccess)
        {
            Console.Clear();
            Console.WriteLine(result.Error);
            char key = BetterConsole.ResponseWithKeyPress("Press any key to try again, or 'q' to cancel operation.");

            if (key.Equals('q'))
                return Result.Failure("Operation canceled.");

            return Handle(request);
        }
        return result;
    }

    public Result<T> Handle<T>(IRequest<Result<T>> request)
    {
        Console.Clear();
        var result = Mediator.Send<Result<T>>(request).Result;
        if (!result.IsSuccess)
        {
            Console.Clear();
            Console.WriteLine(result.Error);
            char key = BetterConsole.ResponseWithKeyPress("Press any key to try again, or 'q' to cancel operation.");

            if (key.Equals('q'))
                return Result<T>.Failure("Operation canceled.");

            return Handle(request);
        }
        return result;
    }
}