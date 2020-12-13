using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Threading.Tasks;
namespace TroubleCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class TroubleGameContainer : BasicGameContainer<TroublePlayerItem, TroubleSaveInfo>
    {
        public TroubleGameContainer(
            BasicData basicData,
            TestOptions test,
            IGameInfo gameInfo,
            IAsyncDelayer delay,
            IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            RandomGenerator random) : base(basicData,
                test,
                gameInfo,
                delay,
                aggregator,
                command,
                resolver,
                random)
        {
        }
        public int MovePlayer { get; set; }
        public int PlayerGoingBack { get; set; }
        public AnimateBasicGameBoard Animates { get; set; } = new AnimateBasicGameBoard(); //hopefully this simple (?)
        public Func<int, bool>? IsValidMove { get; set; }
        public Func<int, Task>? MakeMoveAsync { get; set; }
        public Func<bool>? CanRollDice { get; set; }
        public Func<Task>? RollDiceAsync { get; set; }
    }
}