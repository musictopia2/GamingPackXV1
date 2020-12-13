using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using RummyDiceCP.Data;
using System;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace RummyDiceCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class RummyBoardCP : ObservableObject
    {
        readonly IAsyncDelayer _delay;
        private readonly BasicData _basicData;
        private readonly CommandContainer _command;
        private readonly TestOptions _test;
        readonly IGenerateDice<int> _gens;
        public async Task SelectDiceAsync(RummyDiceInfo dice) //did not need command anymore because of what i did for blazor.
        {
            if (_command.IsExecuting)
            {
                return;
            }
            _command.IsExecuting = true;
            var list = SaveRoot!.Invoke().DiceList;
            int index = list.IndexOf(dice);
            if (index == -1)
            {
                throw new BasicBlankException("had problems hooking up.  Rethink");
            }
            if (_basicData.MultiPlayer == true)
            {
                await _network!.SendAllAsync("diceclicked", index); //i think
            }
            await SelectOneMainAsync!.Invoke(index);
        }
        private readonly INetworkMessages? _network;
        public RummyBoardCP(TestOptions test,
            IGenerateDice<int> gens,
            IAsyncDelayer delay,
            BasicData basicData,
            CommandContainer command
            )
        {
            _test = test;
            _gens = gens; //hopefully putting here is acceptable
            _delay = delay;
            _basicData = basicData;
            _command = command;
            _network = _basicData.GetNetwork();
        }
        #region "Delegates"
        public Func<RummyDiceSaveInfo>? SaveRoot { get; set; } //keep delegates in here.
        public Func<int, Task>? SelectOneMainAsync { get; set; }
        #endregion
        public void EndTurn()
        {
            _doStart = true;
            SaveRoot!.Invoke().DiceList.Clear();
        }
        private bool _doStart = true;
        public void SelectDice(int whichOne)
        {
            SaveRoot!.Invoke().DiceList[whichOne].IsSelected = !SaveRoot!.Invoke().DiceList[whichOne].IsSelected;
        }
        private void UnselectAll()
        {
            SaveRoot!.Invoke().DiceList.UnselectAllObjects();
        }
        public CustomBasicList<CustomBasicList<RummyDiceInfo>> RollDice()
        {
            int newNum;
            if (SaveRoot!.Invoke().RollNumber == 1)
            {
                newNum = 10;
                SaveRoot!.Invoke().DiceList.Clear();
            }
            else
            {
                newNum = SaveRoot!.Invoke().DiceList.Count;
            }
            CustomBasicList<CustomBasicList<RummyDiceInfo>> output = new CustomBasicList<CustomBasicList<RummyDiceInfo>>();
            CustomBasicList<RummyDiceInfo> tempCol;
            CustomBasicList<int> possibleList = _gens.GetPossibleList;
            RummyDiceInfo thisDice;
            7.Times(x =>
            {
                tempCol = new CustomBasicList<RummyDiceInfo>();
                newNum.Times(y =>
                {
                    thisDice = new RummyDiceInfo();
                    thisDice.Populate(possibleList.GetRandomItem());
                    tempCol.Add(thisDice);
                });
                output.Add(tempCol);
            });
            return output;
        }
        public async Task ShowRollingAsync(CustomBasicList<CustomBasicList<RummyDiceInfo>> diceCollection)
        {
            int delay;
            if (_doStart)
            {
                delay = 100;
            }
            else
            {
                delay = 50;
            }
            await diceCollection.ForEachAsync(async thisList =>
            {
                SaveRoot!.Invoke().DiceList.ReplaceRange(thisList);
                //try the same performance improvement.  don't update all but only update the dice portions.


                //CommandContainer.UpdateSpecificAction("dicecup");
                _command.UpdateSpecificAction("rummydice"); //this is only for this game.  no need to accomodate trouble this time.
                //_command.UpdateAll(); //i think here too.
                if (_test.NoAnimations == false)
                {
                    await _delay.DelayMilli(delay);
                }
            });
            SaveRoot!.Invoke().DiceList.Sort();
        }
        public void AddBack(CustomBasicList<RummyDiceInfo> thisList)
        {
            SaveRoot!.Invoke().DiceList.AddRange(thisList);
            if (thisList.Count == 0)
            {
                return;
            }
            UnselectAll();
            SaveRoot!.Invoke().DiceList.Sort();
        }
        public ICustomBasicList<RummyDiceInfo> GetSelectedList()
        {
            ICustomBasicList<RummyDiceInfo> output = SaveRoot!.Invoke().DiceList.RemoveAllAndObtain(Items => Items.IsSelected == true);
            SaveRoot!.Invoke().DiceList.Sort();
            return output;
        }
        public bool HasSelectedDice()
        {
            return SaveRoot!.Invoke().DiceList.Any(Items => Items.IsSelected == true);
        }
    }
}