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
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PassOutDiceGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class PassOutDiceGameGameContainer : BasicGameContainer<PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>
    {
        public PassOutDiceGameGameContainer(
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
        public Dictionary<int, SpaceInfo>? SpaceList { get; set; }
        public Func<int, Task>? MakeMoveAsync { get; set; }
    }
}