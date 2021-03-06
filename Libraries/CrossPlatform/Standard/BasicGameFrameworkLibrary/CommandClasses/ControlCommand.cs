﻿using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public class ControlCommand : IGameCommand
    {
        private readonly MethodInfo _execute;
        protected readonly IControlObservable _model;
        private bool _isAsync;
        private bool _hasParameters;
        public EnumCommandBusyCategory BusyCategory { get; set; }
        protected CommandContainer CommandContainer { get; set; }
        object ICustomCommand.Context => _model; //i think.
        protected virtual void AddCommand()
        {
            CommandContainer.AddCommand(this);
        }
        public ControlCommand(IControlObservable model, MethodInfo execute, CommandContainer container)
        {
            CommandContainer = container;
            _model = model;
            _execute = execute;
            HookUpNotifiers();
        }
        private void HookUpNotifiers()
        {
            BusyCategory = EnumCommandBusyCategory.Limited;
            _isAsync = _execute.ReturnType.Name == "Task";
            var count = _execute.GetParameters().Count();
            if (count > 1)
            {
                throw new BasicBlankException($"Method {_execute.Name} cannot have more than one parameter.  If more than one is needed, lots of rethinking is required");
            }
            _hasParameters = count == 1;
            AddCommand();
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
            return _model.CanExecute();
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