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
namespace SorryCP.Data
{
    [SingletonGame]
    public class SorryGameContainer : BasicGameContainer<SorryPlayerItem, SorrySaveInfo>
    {
        public SorryGameContainer(
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
        public AnimateBasicGameBoard Animates { get; set; } = new AnimateBasicGameBoard(); //needs public so blazor can communicate with this.
        //this time, i want to set delegates to stop the overflows.
        public Func<Task>? DrawClickAsync { get; set; }
        public Func<EnumColorChoice, Task>? HomeClickedAsync { get; set; }
        public Func<int, Task>? SpaceClickedAsync { get; set; }
    }
}