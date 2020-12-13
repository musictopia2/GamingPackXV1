using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Diagnostics;

namespace YachtRaceCP.Data
{
    [SingletonGame]
    public class YachtRaceVMData : ObservableObject, IBasicDiceGamesData<SimpleDice>
    {
        private string _normalTurn = "";
        [VM]
        public string NormalTurn
        {
            get { return _normalTurn; }
            set
            {
                if (SetProperty(ref _normalTurn, value))
                {
                    
                }
            }
        }
        private string _status = "";
        [VM] //use this tag to transfer to the actual view model.  this is being done to avoid overflow errors.
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value))
                {
                    
                }
            }
        }
        private int _rollNumber;
        private readonly IGamePackageResolver _resolver;
        private readonly CommandContainer _command;
        [VM]
        public int RollNumber
        {
            get { return _rollNumber; }
            set
            {
                if (SetProperty(ref _rollNumber, value))
                {
                    
                }
            }
        }
        public DiceCup<SimpleDice>? Cup { get; set; }
        public YachtRaceVMData(IGamePackageResolver resolver, CommandContainer command)
        {
            _resolver = resolver;
            _command = command;
            Stops = new Stopwatch();
        }
        public void LoadCup(ISavedDiceList<SimpleDice> saveRoot, bool autoResume)
        {
            if (Cup != null && autoResume)
            {
                return;
            }
            Cup = new DiceCup<SimpleDice>(saveRoot.DiceList, _resolver, _command);
            if (autoResume == true)
            {
                Cup.CanShowDice = true;
            }
            Cup.HowManyDice = 5; //you specify how many dice here.
            Cup.Visible = true; //i think.
            Cup.ShowHold = true;
        }


        internal Stopwatch Stops { get; set; }


        //looks like doing my custom timer does not work so well for this game.
        //internal Timer GameTimer { get; set; } //decided to use my custom timer.
    }
}