using System.Threading.Tasks;
using Framework.Core;


namespace Framework.Application
{
    public class LogCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;


        public LogCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler)
        {
            _commandHandler = commandHandler;

        }

        public async Task HandleAsync(TCommand command)
        {
         

 

            await _commandHandler.HandleAsync(command);
            
   
        }
    }
}
