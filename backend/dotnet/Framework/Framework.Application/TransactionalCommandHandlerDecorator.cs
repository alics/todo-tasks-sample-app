using Framework.Core;

namespace Framework.Application;

/// <summary>
///     Represents a decorator for a command handler that ensures transactional behavior.
/// </summary>
/// <typeparam name="TCommand">The type of command to be handled.</typeparam>
public class TransactionalCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionalCommandHandlerDecorator{TCommand}" /> class
    ///     with the specified unit of work and command handler.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used to manage the transaction.</param>
    /// <param name="commandHandler">The underlying command handler.</param>
    public TransactionalCommandHandlerDecorator(
        IUnitOfWork unitOfWork,
        ICommandHandler<TCommand> commandHandler)
    {
        _unitOfWork = unitOfWork;
        _commandHandler = commandHandler;
    }

    /// <summary>
    ///     Handles the specified command asynchronously within a transactional scope.
    /// </summary>
    /// <param name="command">The command to be handled.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown when an error occurs during command handling.</exception>
    public async Task HandleAsync(TCommand command)
    {
        try
        {
            await _commandHandler.HandleAsync(command);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}