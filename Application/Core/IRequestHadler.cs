using MediatR;

namespace Application.Core;

public interface IRequestHandler<in TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    TResponse Handle(TRequest request);
}