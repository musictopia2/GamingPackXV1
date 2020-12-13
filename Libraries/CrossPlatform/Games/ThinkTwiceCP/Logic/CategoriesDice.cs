using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Threading.Tasks;
using ThinkTwiceCP.Data;
using static BasicGameFrameworkLibrary.Dice.SharedDiceRoutines;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings;
namespace ThinkTwiceCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class CategoriesDice : ObservableObject, ICompleteSingleDice<string>, IHoldDice
    {
        private bool _hold;
        public bool Hold
        {
            get { return _hold; }
            set
            {
                if (SetProperty(ref _hold, value))
                {
                    _gameContainer.SaveRoot!.CategoryHeld = value;
                }
            }
        }
        public CategoriesDice(ThinkTwiceGameContainer gameContainer)
        {
            _gameContainer = gameContainer;
            MainContainer = _gameContainer.Resolver;
        }
        readonly ThinkTwiceGameContainer _gameContainer;
        public int HeightWidth { get; } = 60;
        public IGamePackageResolver? MainContainer { get; set; }
        private string _value = "";
        public string Value
        {
            get { return _value; }
            set
            {
                if (SetProperty(ref _value, value))
                {
                    switch (value)
                    {
                        case "":
                            _gameContainer.SaveRoot!.CategoryRolled = -1;
                            return;
                        case "D":
                            _gameContainer.SaveRoot!.CategoryRolled = 1;
                            return;
                        case "E":
                            _gameContainer.SaveRoot!.CategoryRolled = 2;
                            return;
                        case "H":
                            _gameContainer.SaveRoot!.CategoryRolled = 3;
                            return;
                        case "L":
                            _gameContainer.SaveRoot!.CategoryRolled = 4;
                            return;
                        case "O":
                            _gameContainer.SaveRoot!.CategoryRolled = 5;
                            return;
                        case "S":
                            _gameContainer.SaveRoot!.CategoryRolled = 6;
                            return;
                        default:
                            throw new BasicBlankException("Value Not supported");
                    }
                }
            }
        }
        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (SetProperty(ref _visible, value))
                {

                }
            }
        }
        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                if (SetProperty(ref _index, value))
                {

                }
            }
        }
        private void FillText(int whichValue)
        {
            if (whichValue == 1)
            {
                Value = "D";
            }
            else if (whichValue == 2)
            {
                Value = "E";
            }
            else if (whichValue == 3)
            {
                Value = "H";
            }
            else if (whichValue == 4)
            {
                Value = "L";
            }
            else if (whichValue == 5)
            {
                Value = "O";
            }
            else if (whichValue == 6)
            {
                Value = "S";
            }
            else
            {
                Value = "U"; //hopefully obvious something is wrong.
            }
        }
        public void LoadSavedGame()
        {
            if (_gameContainer.SaveRoot!.CategoryRolled == -1)
            {
                return;
            }
            FillText(_gameContainer.SaveRoot.CategoryRolled);
            Visible = true;
        }
        public void NewTurn()
        {
            Value = "";
            Visible = false;
            _gameContainer.SaveRoot!.CategorySelected = -1;
        }
        public CustomBasicList<string> GetPossibleList
        {
            get
            {
                WeightedAverageLists<string> thisWeight = new WeightedAverageLists<string>();
                thisWeight.MainContainer = MainContainer!;
                thisWeight.AddWeightedItem("D", 40, 50).AddWeightedItem("E", 20)
                .AddWeightedItem("O", 25, 30)
                .AddWeightedItem("S", 30).AddWeightedItem("H", 5, 10).AddWeightedItem("L", 35, 40);
                return thisWeight.GetWeightedList();
            }
        }
        public async Task ShowRollingAsync(CustomBasicList<string> thisCol)
        {
            if (Hold == true)
            {
                throw new BasicBlankException("Can't show it rolling because you held on to the dice");
            }
            Visible = true;
            await thisCol.ForEachAsync(async category =>
            {
                Populate(category);
                _gameContainer.Command.UpdateAll();
                await _gameContainer.Delay.DelaySeconds(.07);
            });
        }
        public async Task SendMessageAsync(string category, CustomBasicList<string> thisList)
        {
            await _gameContainer.Network!.SendAllAsync(category, thisList);
        }
        public async Task<CustomBasicList<string>> GetDiceList(string content)
        {
            return await js.DeserializeObjectAsync<CustomBasicList<string>>(content);
        }
        public CustomBasicList<string> RollDice(int HowManySections = 6)
        {
            return GetSingleRolledDice(HowManySections, this);
        }
        public void Populate(string chosen)
        {
            Value = chosen;
        }
    }
}