using Framework.Application;
using Microsoft.Extensions.DependencyInjection;
using TodoApplication.ApplicationService;
using TodoApplication.ApplicationService.CommandHandlers.TodoTask;
using TodoApplication.ApplicationService.Contracts.TodoTask;
using TodoApplication.ApplicationService.Ports.Input;
using TodoApplication.Domain.TodoTasks.Adapters;
using TodoApplication.Persistence.TodoTasks.Ports.Output;

namespace TodoApplication.ServicesConfiguration;

public static class TodoApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddTodoApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
        services.AddScoped<ITodoTaskApplicationService, TodoTaskApplicationService>();

        services.AddScoped<ICommandHandler<CreateTodoTaskCommand>, CreateTodoTaskCommandHandler>();
        services
            .Decorate<ICommandHandler<CreateTodoTaskCommand>,
                TransactionalCommandHandlerDecorator<CreateTodoTaskCommand>>();

        services.AddScoped<ICommandHandler<UpdateTodoTaskCommand>, UpdateTodoTaskCommandHandler>();
        services
            .Decorate<ICommandHandler<UpdateTodoTaskCommand>,
                TransactionalCommandHandlerDecorator<UpdateTodoTaskCommand>>();

        services.AddScoped<ICommandHandler<DeleteTodoTaskCommand>, DeleteTodoTaskCommandHandler>();
        services
            .Decorate<ICommandHandler<DeleteTodoTaskCommand>,
                TransactionalCommandHandlerDecorator<DeleteTodoTaskCommand>>();

        return services;
    }
}