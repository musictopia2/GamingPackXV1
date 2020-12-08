using BasicGameFrameworkLibrary.CommandClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
namespace BasicGameFrameworkLibrary.MiscProcesses
{
    public abstract class SimpleControlObservable : ObservableObject, IControlObservable
    {
        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (CanEnableFirst() == false)
                    value = false;// otherwise, value can always be false.
                if (SetProperty(ref _isEnabled, value) == true)
                    EnableChange();
            }
        }
        public EnumCommandBusyCategory BusyCategory { get; set; } = EnumCommandBusyCategory.None; //most of the time, none.
        public bool AlwaysDisabled { set; private get; }
        protected CommandContainer CommandContainer;
        public SimpleControlObservable(CommandContainer container)
        {
            CommandContainer = container;
            CommandContainer.AddControl(this); //i think it should be here instead.
        }
        protected virtual bool CanEnableFirst()
        {
            return !AlwaysDisabled;
        }
        public void ManualChange()
        {
            EnableChange();
        }
        protected abstract void EnableChange();
        protected virtual bool SpecialEnable()
        {
            return true;
        }
        protected IBasicEnableProcess? _networkProcess;
        private IEnableAlways? _alwaysProcess;
        protected Func<bool>? _customFunction; // i think this will have no parameters for this one.
        protected bool _useSpecial;
        public virtual void SendEnableProcesses(IBasicEnableProcess nets, Func<bool> fun)
        {
            SendFunction(fun);
            _useSpecial = true;
            _networkProcess = nets;
        }
        public void SendFunction(Func<bool> fun)
        {
            _useSpecial = true;
            _customFunction = fun;
        }
        public void SendAlwaysEnable(IEnableAlways always)
        {
            _alwaysProcess = always;
            BusyCategory = EnumCommandBusyCategory.Limited;
            SetCommandsLimited();
        }
        protected virtual void SetCommandsLimited() { }
        protected abstract void PrivateEnableAlways(); //everything needs to decide what to do about it.
        public void ReportCanExecuteChange()
        {
            IsEnabled = true;
            if (CommandContainer.IsExecuting == true && BusyCategory == EnumCommandBusyCategory.None)
            {
                IsEnabled = false; //i think
                return;
            }
            if (CommandContainer.Processing == true && BusyCategory == EnumCommandBusyCategory.Limited)
            {
                IsEnabled = false;
                return;
            }
            if (_useSpecial == false)
            {
                if (AlwaysDisabled == true)
                {
                    IsEnabled = false;
                }
                else
                {
                    IsEnabled = true; //for now until i figure out further.
                }
                return;
            }
            if (_alwaysProcess != null)
            {
                IsEnabled = _alwaysProcess.CanEnableAlways();
                return;
            }
            if (_networkProcess != null)
            {
                if (_networkProcess.CanEnableBasics() == false)
                {
                    IsEnabled = false;
                    return;
                }
            }
            IsEnabled = _customFunction!();
        }
        bool IControlObservable.CanExecute()
        {
            if (IsEnabled == false)
            {
                return false;
            }
            return SpecialEnable();
        }
    }
}