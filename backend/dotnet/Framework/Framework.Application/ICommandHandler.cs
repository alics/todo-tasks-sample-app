using Framework.Core;

namespace Framework.Application;

/// <summary>
///     Represents a handler for a specific type of command.
/// </summary>
/// <typeparam name="TCommand">The type of command to be handled.</typeparam>
public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    /// <summary>
    ///     Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to be handled.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task HandleAsync(TCommand command);
}