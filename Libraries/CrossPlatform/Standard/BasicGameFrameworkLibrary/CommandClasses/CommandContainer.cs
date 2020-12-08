using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public class CommandContainer
    {
        private readonly CustomBasicList<IGameCommand> _commandList = new CustomBasicList<IGameCommand>();
        private readonly CustomBasicList<IGameCommand> _openList = new CustomBasicList<IGameCommand>();
        public event ExecutingChangedEventHandler? ExecutingChanged; //don't need anything else because can call a method to see where it stands.
        public delegate void ExecutingChangedEventHandler(); //i think
        private readonly CustomBasicList<IControlObservable> _controlList = new CustomBasicList<IControlObservable>();
        private readonly CustomBasicList<Action> _allActions = new CustomBasicList<Action>(); //this is not static though like the other one.
        private readonly Dictionary<string, Action> _specialActions = new Dictionary<string, Action>();
        public CommandContainer()
        {
            IsExecuting = true; //i think it needs to start out as true.
        }

        //public bool Experiment { get; set; }

        public void UpdateAll() //this will update all no matter what.
        {
            //if (BasicData.IsWasm && Experiment)
            //{
            //    return; //for now no updates for web assembly.  if needed, rethink.
            //}
            //with mobile bindings, its really hosed.  also does not work great with web assembly.
            var list = _allActions.ToCustomBasicList();
            list.ForEach(a =>
            {
                if (a != null)
                {
                    a.Invoke();
                }
            });
        }
        public void UpdateSpecificAction(string key)
        {
            if (_specialActions.ContainsKey(key) == false)
            {
                return; //because there is nothing.
            }
            _specialActions[key].Invoke();
        }
        public bool CanExecuteManually()
        {
            return !IsExecuting;
        }
        public void StartExecuting() //this goes to the update actions.
        {
            IsExecuting = true;
            Processing = true;
        }
        public async Task ProcessCustomCommandAsync<T>(Func<T, Task> action, T argument)
        {
            StartExecuting();
            await action.Invoke(argument);
            StopExecuting();
        }
        public async Task ProcessCustomCommandAsync(Func<Task> action)
        {
            StartExecuting();
            await action.Invoke();
            StopExecuting();
        }
        public void StopExecuting()
        {
            if (ManuelFinish == false)
            {
                IsExecuting = false;
            }
            Processing = false;
        }
        public void ClearLists()
        {
            _commandList.Clear();
            _openList.Clear();
            _controlList.Clear();
            _allActions.Clear(); //i think
        }
        private bool _executing;
        /// <summary>
        /// This is used when its not even your turn.
        /// Use Processing if you are able to do things out of turn as long
        /// as the other variable is false.
        /// </summary>
        /// <remarks>This is used when its not even your turn.
        /// Use Processing if you are able to do things out of turn as long
        /// as the other variable is false.</remarks>
        public bool IsExecuting
        {
            get
            {
                return _executing;
            }
            set
            {

                if (value == _executing)
                {
                    return;
                }
                _executing = value;
                ReportAll();
            }
        }
        private bool _openBusy;
        public bool OpenBusy
        {
            get
            {
                return _openBusy;
            }
            set
            {
                if (value == _openBusy)
                {
                    return;
                }
                _openBusy = value;
                ReportOpen();
            }
        }
        public void ReportOpen() //iffy
        {
            _openList.ForEach(x => x.ReportCanExecuteChange()); //i think this simple.
            UpdateAll(); //here too now.
        }
        private bool _processing = true; //you need to start out that its processing.
        public bool Processing
        {
            get { return _processing; }
            set
            {
                if (value == _processing)
                {
                    return;
                }
                _processing = value;
                ReportLimited();
            }
        }
        public void ReportLimited() //iffy
        {
            ReportItems(EnumCommandBusyCategory.Limited);
            UpdateAll();
        }
        public bool ManuelFinish { get; set; } = false;
        public void ManualReport()
        {
            _commandList.ForEach(x => x.ReportCanExecuteChange());
            _controlList.ForEach(x => x.ReportCanExecuteChange());
        }
        public void RemoveOldItems(object payLoad)
        {
            _commandList.RemoveAllOnly(x => x.Context == payLoad);
        }
        public void ReportAll() //when changing, will report to all no matter what.  decided it can be good to notify all that something has changed.
        {
            ReportItems(EnumCommandBusyCategory.None);
            if (ExecutingChanged != null)
            {
                ExecutingChanged.Invoke();
            }
            UpdateAll();
        }
        private void ReportItems(EnumCommandBusyCategory thisBusy)
        {
            _commandList.ForConditionalItems(items => items.BusyCategory == thisBusy
            , items => items.ReportCanExecuteChange());
            _controlList.ForConditionalItems(items => items.BusyCategory == thisBusy
            , items => items.ReportCanExecuteChange());
        }
        public void AddOpen(IGameCommand thisOpen, Action action)
        {
            _openList.Add(thisOpen);
            AddAction(action);
        }
        public void AddCommand(IGameCommand thisCommand, Action action)
        {
            _commandList.Add(thisCommand);
            AddAction(action);
        }
        public void AddCommand(IGameCommand command)
        {
            _commandList.Add(command); //hopefully the action was already recorded anyways (?)
        }
        public void AddControl(IControlObservable thisControl, Action action)
        {
            AddControl(thisControl);
            AddAction(action);
        }
        public void AddControl(IControlObservable thisControl)
        {
            _controlList.Add(thisControl);
        }
        public void AddAction(Action action)
        {
            if (_allActions.Contains(action) == false)
            {
                _allActions.Add(action);
            }
            else
            {

            }
        }
        public void AddAction(Action action, string key)
        {
            if (_specialActions.ContainsKey(key) == false)
            {
                _specialActions.Add(key, action);
            }
        }
        public void RemoveCommand(IGameCommand command, Action action)
        {
            _commandList.RemoveSpecificItem(command);
            RemoveAction(action);
        }
        public void RemoveAction(Action action)
        {
            _allActions.RemoveSpecificItem(action); //sometimes this is needed now too.
        }
        public void RemoveAction(string key)
        {
            if (_specialActions.ContainsKey(key) == false)
            {
                return;
            }
            _specialActions.Remove(key); //i think
        }
    }
}