using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public class OutOfTurnCommand : IGameCommand
    {
        private readonly MethodInfo _execute;
        protected readonly IEnableAlways _model;
        private bool _isAsync;
        private bool _hasParameters;
        public EnumCommandBusyCategory BusyCategory { get; set; }
        protected CommandContainer CommandContainer { get; set; }
        protected Action UpdateBlazor { get; set; }
        object ICustomCommand.Context => _model; //i think.
        public OutOfTurnCommand(IEnableAlways model, MethodInfo execute, Action afterChange, CommandContainer container)
        {
            UpdateBlazor = afterChange;
            CommandContainer = container;
            _model = model;
            _execute = execute;
            HookUpNotifiers();
        }
        private void HookUpNotifiers()
        {
            BusyCategory = EnumCommandBusyCategory.Limited;
            if (UpdateBlazor != null)
            {
                CommandContainer.AddCommand(this, UpdateBlazor);
            }
            else
            {
                CommandContainer.AddCommand(this);
            }
            _isAsync = _execute.ReturnType.Name == "Task";
            var count = _execute.GetParameters().Count();
            if (count > 1)
            {
                throw new BasicBlankException($"Method {_execute.Name} cannot have more than one parameter.  If more than one is needed, lots of rethinking is required");
            }
            _hasParameters = count == 1;
        }

        public event EventHandler CanExecuteChanged = delegate { };
        public void ReportCanExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        protected virtual void StartExecuting()
        {
            CommandContainer.StartExecuting();
        }
        protected virtual void StopExecuting()
        {
            CommandContainer.StopExecuting();
        }
        public virtual bool CanExecute(object? parameter)
        {
            if (CommandContainer.IsExecuting == true && BusyCategory == EnumCommandBusyCategory.None)
            {
                return false;
            }
            if (CommandContainer.Processing == true && BusyCategory == EnumCommandBusyCategory.Limited)
            {
                return false;
            }
            return _model.CanEnableAlways();
        }

        public async Task ExecuteAsync(object? parameter)
        {
            if (CanExecute(parameter) == false)
            {
                return;
            }
            StartExecuting();
            if (_isAsync == false)
            {
                if (_hasParameters)
                {
                    _execute.Invoke(_model, new object[] { parameter! });
                }
                else
                {
                    _execute.Invoke(_model, null); //hopefully this works too.
                }
            }
            else
            {
                Task task;
                if (_hasParameters)
                {
                    task = (Task)_execute.Invoke(_model, new object[] { parameter! });
                }
                else
                {
                    task = (Task)_execute.Invoke(_model, null);
                }

                await task;
            }
            StopExecuting();
        }
    }
}