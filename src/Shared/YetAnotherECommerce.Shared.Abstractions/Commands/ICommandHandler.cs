using System.Threading.Tasks;

namespace YetAnotherECommerce.Shared.Abstractions.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}