using Framework.Core;

namespace Framework.Application;

/// <summary>
///     Represents a base class for command handlers, providing a common interface for handling commands.
/// </summary>
/// <typeparam name="TCommand">The type of command to be handled.</typeparam>
public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandHandlerBase{TCommand}" /> class with the specified unit of
    ///     work.
    /// </summary>
    /// <param name="unitOfWork">The unit of work to be used within the command handler.</param>
    protected CommandHandlerBase(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to be handled.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public abstract Task HandleAsync(TCommand command);
}