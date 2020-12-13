using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
namespace CountdownCP.Data
{
    [SingletonGame]
    public class CountdownVMData : ObservableObject, IBasicDiceGamesData<CountdownDice>
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
        private int _round; //this is needed because the game has to end at some point no matter what even if tie.
        [VM]
        public int Round
        {
            get { return _round; }
            set
            {
                if (SetProperty(ref _round, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        public DiceCup<CountdownDice>? Cup { get; set; }
        public CountdownVMData(IGamePackageResolver resolver, CommandContainer command)
        {
            _resolver = resolver;
            _command = command;
        }
        public void LoadCup(ISavedDiceList<CountdownDice> saveRoot, bool autoResume)
        {
            if (Cup != null && autoResume)
            {
                return;
            }
            Cup = new DiceCup<CountdownDice>(saveRoot.DiceList, _resolver, _command);
            if (autoResume == true)
            {
                Cup.CanShowDice = true;
            }
            Cup.HowManyDice = 2;
            Cup.Visible = true;
        }
        public static bool ShowHints { get; set; }
        public static Func<SimpleNumber, bool>? CanChooseNumber { get; set; }
    }
}