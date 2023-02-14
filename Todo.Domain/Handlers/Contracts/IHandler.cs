using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Handlers.Contracts;

public interface IHandler<T> where T : ICommand //Exige que T seja do tipo ICommand
{
  ICommandResult Handle(T command);
}