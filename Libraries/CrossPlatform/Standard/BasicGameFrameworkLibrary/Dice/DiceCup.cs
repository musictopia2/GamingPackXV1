using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace BasicGameFrameworkLibrary.Dice
{
    public class DiceCup<D> : SimpleControlObservable, IRollMultipleDice<D> where D :
        IStandardDice, new()
    {
        public IGamePackageResolver? MainContainer { get; set; }
        private IAsyncDelayer? _delay;

        public event Func<D, Task>? DiceClickedAsync;

        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (SetProperty(ref _visible, value))
                {
                    //can decide what to do when property changes
                }

            }
        }

        private async Task PrivateDiceClickAsync(D dice)
        {
            if (DiceClickedAsync == null)
            {
                return;
            }
            await DiceClickedAsync.Invoke(dice);
        }

        public ControlCommand DiceCommand { get; set; } //this needs to be old fashioned.  that seemed to work well.

        private readonly INetworkMessages? _network;
        public DiceCup(DiceList<D> PrivateList, IGamePackageResolver resolver, CommandContainer command) : base(command)
        {
            DiceList = PrivateList;
            BasicData thisData = resolver.Resolve<BasicData>();
            MainContainer = resolver;
            DiceList.MainContainer = MainContainer;
            if (thisData.MultiPlayer == true)
            {
                _network = resolver.Resolve<INetworkMessages>();
            }

            MethodInfo method = this.GetPrivateMethod(nameof(PrivateDiceClickAsync));
            DiceCommand = new ControlCommand(this, method, command);
        }
        private bool _canShowDice;
        public bool CanShowDice
        {
            get { return _canShowDice; }
            set
            {
                if (SetProperty(ref _canShowDice, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        public int TotalDiceValue => DiceList.Sum(Items => Items.Value); //this is so common might as well have a routine for it.
        public DiceList<D> DiceList { get; } //only because its needed for the wpf/xamarin forms part.
        int _originalNumber;
        private int _howManyDice;
        public int HowManyDice
        {
            get { return _howManyDice; }
            set
            {
                if (SetProperty(ref _howManyDice, value))
                {
                    if (value > _originalNumber)
                    {
                        _originalNumber = value;
                    }
                }
            }
        }
        private bool _hasDice;
        public bool HasDice
        {
            get { return _hasDice; }
            set
            {
                if (SetProperty(ref _hasDice, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        private bool _showDiceListAlways;
        public bool ShowDiceListAlways
        {
            get { return _showDiceListAlways; }
            set
            {
                if (SetProperty(ref _showDiceListAlways, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        public bool ShowHold { get; set; } //i think this does not need to pass on information to the view model.
        public void SelectUnselectDice(int index) //since i have shortcut, if i do through another way, it will be allowed.
        {
            if (ShowHold == true)
            {
                throw new BasicBlankException("The dice is being held, not selected");
            }
            D thisDice = DiceList.Single(Items => Items.Index == index);
            thisDice.IsSelected = !thisDice.IsSelected;
        }
        public void HoldUnholdDice(int index)
        {
            if (ShowHold == false)
            {
                throw new BasicBlankException("The dice is being selected, not held");
            }
            D thisDice = DiceList[index - 1];
            thisDice.Hold = !thisDice.Hold;
        }
        public void UnholdDice() => DiceList.ForEach(items => items.Hold = false);
        public bool IsDiceHeld(int index)
        {
            if (ShowHold == false)
            {
                throw new BasicBlankException("The dice is being selected, not held");
            }
            return DiceList[index].Hold;
        }
        public int HowManyHeldDice()
        {
            if (ShowHold == false)
            {
                throw new BasicBlankException("The dice is being selected, not held");
            }
            return DiceList.Count(Items => Items.Hold);
        }
        public bool HasSelectedDice()
            => DiceList.Exists(items => items.IsSelected == true);
        public CustomBasicList<D> ListSelectedDice()
        {
            if (ShowHold == true)
            {
                throw new BasicBlankException("The dice is being held, not selected");
            }
            return DiceList.GetSelectedItems();
        }
        public async Task<CustomBasicList<CustomBasicList<D>>> GetDiceList(string body)
        {
            return await js.DeserializeObjectAsync<CustomBasicList<CustomBasicList<D>>>(body);
        }
        public CustomBasicList<CustomBasicList<D>> RollDice(int howManySections = 6)
        {
            if (DiceList.Count() != HowManyDice)
                RedoList();
            CustomBasicList<CustomBasicList<D>> output = new CustomBasicList<CustomBasicList<D>>();
            AsyncDelayer.SetDelayer(this, ref _delay!); //try both places.
            IGenerateDice<int> ThisG = MainContainer!.Resolve<IGenerateDice<int>>();
            CustomBasicList<int> possList = ThisG.GetPossibleList;
            possList.MainContainer = MainContainer;
            D tempDice;
            int chosen;
            howManySections.Times(() =>
            {
                CustomBasicList<D> firsts = new CustomBasicList<D>();
                for (int i = 0; i < HowManyDice; i++)
                {
                    tempDice = DiceList[i];
                    if (tempDice.Hold == false) //its uncommon enough that has to be different for different types of dice games.
                    {
                        chosen = possList.GetRandomItem();
                        tempDice = new D();
                        tempDice.Index = i + 1; //i think
                        tempDice.Populate(chosen); //so they can do what they need to.
                    }
                    firsts.Add(tempDice);
                }
                output.Add(firsts);
            });
            return output;
        }
        public void ClearDice()
        {
            HasDice = true;
            HowManyDice = _originalNumber;
            DiceList.Clear(HowManyDice);
        }
        private void SetContainer()
        {
            if (MainContainer == null)
            {
                throw new BasicBlankException("Needs container in order to clear dice");
            }
            if (DiceList.MainContainer == null)
            {
                DiceList.MainContainer = MainContainer; //try this too.
            }
        }
        private void RedoList()
        {
            DiceList.Clear(HowManyDice);
        }

        public async Task SendMessageAsync(CustomBasicList<CustomBasicList<D>> thisList)
        {
            await _network!.SendAllAsync("rolled", thisList); //i think
        }
        public async Task SendMessageAsync(string Category, CustomBasicList<CustomBasicList<D>> thisList)
        {
            await _network!.SendAllAsync(Category, thisList); //i think
        }

        public Action? UpdateDiceAction { get; set; }

        public async Task ShowRollingAsync(CustomBasicList<CustomBasicList<D>> diceCollection, bool showVisible)
        {
            CanShowDice = showVisible;
            AsyncDelayer.SetDelayer(this, ref _delay!); //because for multiplayer, they do this part but not the other.
            await diceCollection.ForEachAsync(async firsts =>
            {
                DiceList.ReplaceDiceRange(firsts);
                int tempCount = DiceList.Count;
                if (DiceList.Any(Items => Items.Index > tempCount || Items.Index <= 0))
                {
                    throw new BasicBlankException("Index cannot be higher than the dicecount or less than 1");
                }
                HasDice = true;
                if (CanShowDice == true)
                {
                    Visible = true;
                    //will attempt do update a specific one (dice).
                    //however, wants to make it flexible enough that it can be used for other purposes as well.
                    //probably using a dictionary.
                    //CommandContainer.UpdateAll();
                    if (UpdateDiceAction == null)
                    {
                        CommandContainer.UpdateSpecificAction("dicecup");
                    }
                    else
                    {
                        UpdateDiceAction.Invoke(); //to accomodate trouble game.
                    }
                    await _delay.DelayMilli(50);
                }
            });
        }
        public async Task ShowRollingAsync(CustomBasicList<CustomBasicList<D>> thisCol)
        {
            await ShowRollingAsync(thisCol, true);
        }
        public void ReplaceDiceRange(ICustomBasicList<D> thisList)
        {
            DiceList.ReplaceDiceRange(thisList);
            HowManyDice = DiceList.Count;
        }
        public void ReplaceSelectedDice()
        {
            CustomBasicList<D> TempList = DiceList.GetSelectedItems();
            DiceList.ReplaceDiceRange(TempList);
            HowManyDice = DiceList.Count;
        }
        public int ValueOfOnlyDice => DiceList.Single().Value;
        public void RemoveSelectedDice()
        {
            if (ShowHold == true)
            {
                throw new BasicBlankException("Cannot remove selected dice because its being held instead");
            }
            DiceList.RemoveSelectedDice();
            HowManyDice = DiceList.Count;
        }
        public void RemoveConditionalDice(Predicate<D> predicate)
        {
            DiceList.RemoveConditionalDice(predicate);
            HowManyDice = DiceList.Count;
        }
        public void HideDice() //could be iffy now (?)
        {
            HasDice = false;
        }
        protected override void EnableChange()
        {
            DiceCommand.ReportCanExecuteChange();
        }
        protected override void PrivateEnableAlways() { }
    }
}