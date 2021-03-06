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
namespace ChineseCheckersCP.Data
{
    [SingletonGame]
    public class ChineseCheckersGameContainer : BasicGameContainer<ChineseCheckersPlayerItem, ChineseCheckersSaveInfo>
    {
        public ChineseCheckersGameContainer(
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
            Animates = new AnimateBasicGameBoard();
        }
        public Func<int, Task>? MakeMoveAsync { get; set; }
        public AnimateBasicGameBoard Animates { get; set; }
        public ChineseCheckersVMData? Model { get; set; }
    }
}