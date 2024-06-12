using Framework.Core;

namespace Framework.Application;

/// <summary>
///     Represents a command bus responsible for dispatching commands to their respective command handlers.
/// </summary>
public interface ICommandBus
{
    /// <summary>
    ///     Dispatches the specified command to its corresponding command handler asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be dispatched.</typeparam>
    /// <param name="command">The command to be dispatched.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task DispatchAsync<TCommand>(TCommand command)
        where TCommand : ICommand;
}