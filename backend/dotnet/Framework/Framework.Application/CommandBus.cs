using Framework.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application;

/// <summary>
///     Represents a command bus responsible for dispatching commands to their respective command handlers.
/// </summary>
public class CommandBus : ICommandBus
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandBus" /> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve command handlers.</param>
    public CommandBus(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     Dispatches the specified command to its corresponding command handler asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be dispatched.</typeparam>
    /// <param name="command">The command to be dispatched.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when the command handler for the specified command type is not
    ///     registered.
    /// </exception>
    public async Task DispatchAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        var handler = serviceProvider.GetService<ICommandHandler<TCommand>>();

        if (handler == null)
            throw new ArgumentNullException($"No command handler found for command type {typeof(TCommand)}.");

        await handler.HandleAsync(command);
    }
}