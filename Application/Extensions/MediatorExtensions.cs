using MediatR;

namespace Application.Extensions
{
    public static class MediatorExtensions
    {
        public static TResponse SendToHandler<TResponse>(this IMediator mediator, IRequest<TResponse> request)
        {
            Task<TResponse> t = mediator.Send(request);
            t.Start();
            t.Wait();
            return t.Result;
        }
    }
}