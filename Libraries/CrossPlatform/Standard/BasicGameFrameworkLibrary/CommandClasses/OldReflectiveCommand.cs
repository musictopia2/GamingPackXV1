using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Commands;
using CommonBasicStandardLibraries.MVVMFramework.EventArgClasses;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public class OldReflectiveCommand : ICustomCommand
    {
        private readonly PropertyInfo? _canExecutep;
        private readonly MethodInfo _execute;
        private readonly object _model;
        private readonly MethodInfo? _canExecuteM;
        private bool _isAsync;
        private readonly string _functionName = "";
        private bool _hasParameters;
        object ICustomCommand.Context => _model;
        public Action? UpdateBlazor { get; set; }
        public OldReflectiveCommand(object model, MethodInfo execute, MethodInfo canExecuteM, Action afterChange)
        {
            _model = model;
            UpdateBlazor = afterChange;
            _execute = execute;
            _canExecuteM = canExecuteM;
            if (_canExecuteM != null)
                _functionName = canExecuteM.Name;
            HookUpNotifiers();
        }
        public OldReflectiveCommand(object model, MethodInfo execute, PropertyInfo? canExecute, Action afterChange)
        {
            _model = model;
            _execute = execute;
            _canExecutep = canExecute;
            UpdateBlazor = afterChange;
            if (_canExecutep != null)
                _functionName = _canExecutep.Name;
            HookUpNotifiers();
        }
        private void HookUpNotifiers()
        {
            _isAsync = _execute.ReturnType.Name == "Task";
            var count = _execute.GetParameters().Count();
            if (count > 1)
            {
                throw new BasicBlankException($"Method {_execute.Name} cannot have more than one parameter.  If more than one is needed, lots of rethinking is required");
            }
            _hasParameters = count == 1;
            if (_canExecuteM == null && _canExecutep == null)
            {
                return; //no need to notify because the resulting part is not even there.
            }
            if (_model is INotifyCanExecuteChanged notifier)
            {
                notifier.CanExecuteChanged += Notifier_CanExecuteChanged;
            }
        }
        private void Notifier_CanExecuteChanged(object sender, CanExecuteChangedEventArgs e)
        {
            if (_functionName == "")
            {
                throw new BasicBlankException("No canexecute function was found.  Should not have raised this.  Rethink");
            }
            if (e.Name == _functionName)
            {
                ReportCanExecuteChange();
            }
        }
        private void Notifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_canExecutep == null)
            {
                throw new BasicBlankException("Only properties should have shown property change to let listenrs know canexecute changed");
            }
            if (e.PropertyName == _canExecutep.Name || e.PropertyName == "") //because refresh needs to refresh all period.
            {
                ReportCanExecuteChange();
            }
        }

        private bool _isExecuting;

        public event EventHandler CanExecuteChanged = delegate { };
        public bool CanExecute(object? parameter)
        {
            if (_isExecuting)
            {
                return false;
            }
            //can have extra things that runj before even running this (but not yet).
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
        public void ReportCanExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public async Task ExecuteAsync(object? parameter)
        {
            if (CanExecute(parameter!) == false)
            {
                return;
            }
            _isExecuting = true;
            UpdateBlazor?.Invoke();
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
            _isExecuting = false;
            UpdateBlazor?.Invoke();
        }
    }
}