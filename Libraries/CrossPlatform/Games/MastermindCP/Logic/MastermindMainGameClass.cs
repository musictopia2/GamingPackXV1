using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using MastermindCP.Data;
using MastermindCP.ViewModels;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace MastermindCP.Logic
{
    [SingletonGame]
    public class MastermindMainGameClass : IAggregatorContainer, IFinishGuess
    {
        private readonly GlobalClass _global;
        private readonly LevelClass _level;
        public MastermindMainGameClass(IEventAggregator aggregator,
            GlobalClass global,
            LevelClass level
            )
        {
            Aggregator = aggregator;
            _global = global;
            _level = level;
        }


        public IEventAggregator Aggregator { get; }

        public async Task NewGameAsync(GameBoardViewModel guess)
        {
            //hopefully no need to hide solution anymore since another class is responsible for it now.
            bool canRepeat = _level.LevelChosen == 2 || _level.LevelChosen == 4 || _level.LevelChosen == 6;
            int level = _level.LevelChosen;
            CustomBasicList<Bead> possibleList = new CustomBasicList<Bead>();
            if (level == 5 || level == 6)
            {
                possibleList.Add(new Bead(EnumColorPossibilities.Aqua));
                possibleList.Add(new Bead(EnumColorPossibilities.Black));
            }
            possibleList.Add(new Bead(EnumColorPossibilities.Blue));
            possibleList.Add(new Bead(EnumColorPossibilities.Green));
            if (level > 2)
                possibleList.Add(new Bead(EnumColorPossibilities.Purple));
            possibleList.Add(new Bead(EnumColorPossibilities.Red));
            possibleList.Add(new Bead(EnumColorPossibilities.White));
            if (level > 2)
                possibleList.Add(new Bead(EnumColorPossibilities.Yellow));
            ICustomBasicList<Bead> tempList;
            if (canRepeat == false)
                tempList = possibleList.GetRandomList(false, 4);
            else
            {
                int x;
                tempList = new CustomBasicList<Bead>();
                for (x = 1; x <= 4; x++)
                {
                    var ThisBead = possibleList.GetRandomItem();
                    tempList.Add(ThisBead); // can have repeat
                }
            }
            _global.Solution = tempList.ToCustomBasicList();
            await guess.NewGameAsync();
            await guess.StartNewGuessAsync();
            _global.ColorList = possibleList.Select(items => items.ColorChosen).ToCustomBasicList();
        }

        public async Task GiveUpAsync()
        {
            ToastPlatform.ShowWarning("Sorry you are giving up");
            await this.SendGameOverAsync(); //hopefully this works.
            //Aggregator.ShowSolution(); //does not care who responds to showing solution.
            //i propose a new view model for the solution part.

        }

        async Task IFinishGuess.FinishGuessAsync(int howManyCorrect, GameBoardViewModel board)
        {
            bool handled = false;
            if (howManyCorrect == 4)
            {
                ToastPlatform.ShowSuccess("Congratuations, you won");
                handled = true;
            }
            if (board.GuessList.Last().IsCompleted)
            {
                ToastPlatform.ShowWarning("You ran out of guesses."); //maybe warning.  if i need something else, rethink.
                handled = true;
            }
            if (handled)
            {
                await this.SendGameOverAsync();
                return;
            }
            await board.StartNewGuessAsync();
        }
    }
}