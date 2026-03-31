using MediatR;

namespace EurekaBack.Application.Common
{
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult> { }

    public interface ICommand<out TResult> : IRequest<TResult> { }
}
