using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks;
using ThinkTwiceCP.Data;
namespace ThinkTwiceCP.ViewModels
{
    [InstanceGame]
    public class ScoreViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        private int _itemSelected;
        private readonly ThinkTwiceVMData _model;
        private readonly ThinkTwiceGameContainer _gameContainer;
        public int ItemSelected
        {
            get { return _itemSelected; }
            set
            {
                if (SetProperty(ref _itemSelected, value))
                {

                }
            }
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (SetProperty(ref _isEnabled, value))
                {
                    
                }
            }
        }
        private string GetDescriptionText()
        {
            if (ItemSelected == 5)
            {
                return "2 of a kind:  10 points" + Constants.vbCrLf + "3 of a kind:  20 points" + Constants.vbCrLf + "4 of a kind:  30 points" + Constants.vbCrLf + "5 of a kind:  50 points" + Constants.vbCrLf + "6 of a kind:  100 points";
            }
            return "Sum of all the dice";
        }
        public ScoreViewModel(ThinkTwiceVMData model, ThinkTwiceGameContainer gameContainer)
        {
            _model = model;
            _gameContainer = gameContainer;
            _gameContainer.Command.ExecutingChanged += Command_ExecutingChanged;
            _model.PropertyChanged += PropertyChange;
            CommandContainer = _gameContainer.Command;
            ItemSelected = _model.ItemSelected;
        }
        private bool CanClickCommands(bool isMain)
        {
            if (_gameContainer.SaveRoot.CategoryRolled == -1)
            {
                return false;
            }
            if (isMain == false)
            {
                return true;
            }
            return ItemSelected > -1;
        }
        private void PropertyChange(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemSelected))
            {
                ItemSelected = _model.ItemSelected;
            }
        }
        protected override Task TryCloseAsync()
        {
            _gameContainer.Command.ExecutingChanged -= Command_ExecutingChanged;
            _model.PropertyChanged -= PropertyChange;
            return base.TryCloseAsync();
        }
        private void Command_ExecutingChanged()
        {
            IsEnabled = !_gameContainer.Command.IsExecuting;
        }
        #region Command Functions
        public bool CanScoreDescription => CanClickCommands(true);
        [Command(EnumCommandCategory.Plain)]
        public async Task ScoreDescriptionAsync()
        {
            if (ItemSelected == -1)
            {
                throw new BasicBlankException("Nothing Selected");
            }
            var text = GetDescriptionText();
            await UIPlatform.ShowMessageAsync(text);
        }
        public bool CanChangeSelection => CanClickCommands(false);
        [Command(EnumCommandCategory.Plain)]
        public void ChangeSelection(string text)
        {
            _model.ItemSelected = _model.TextList.IndexOf(text);
        }
        public bool CanCalculateScore => CanClickCommands(true);

        public CommandContainer CommandContainer { get; set; }
        [Command(EnumCommandCategory.Plain)]
        public async Task CalculateScoreAsync()
        {
            if (_gameContainer.ScoreClickAsync == null)
            {
                throw new BasicBlankException("Nobody is handling the score click.  Rethink");
            }
            await _gameContainer.ScoreClickAsync.Invoke();
        }
        #endregion
    }
}