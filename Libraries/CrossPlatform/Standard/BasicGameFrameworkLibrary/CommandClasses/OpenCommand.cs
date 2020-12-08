using System;
using System.Reflection;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public class OpenCommand : PlainCommand
    {
        protected override void AddCommand()
        {
            CommandContainer.AddOpen(this, UpdateBlazor);
        }
        protected override void StartExecuting()
        {
            CommandContainer.OpenBusy = true;
        }
        protected override void StopExecuting() { }
        public override bool CanExecute(object? parameter)
        {
            if (CommandContainer.OpenBusy == true)
            {
                return false;
            }
            if (_canExecutep != null)
            {
                return (bool)_canExecutep.GetValue(_model); //properties cannot have parameters obviously.
            }

            if (_canExecuteM != null)
            {
                if (parameter == null)
                {
                    return (bool)_canExecuteM.Invoke(_model, null);
                }
                return (bool)_canExecuteM.Invoke(_model, new object[] { parameter });
            }
            return true;
        }

        public OpenCommand(object model, MethodInfo execute, MethodInfo canExecuteM, Action action, CommandContainer container) : base(model, execute, canExecuteM, action, container)
        {
        }
        public OpenCommand(object model, MethodInfo execute, PropertyInfo? canExecute, Action action, CommandContainer container) : base(model, execute, canExecute, action, container)
        {
        }
    }
}