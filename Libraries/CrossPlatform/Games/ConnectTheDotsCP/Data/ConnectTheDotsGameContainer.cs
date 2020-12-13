using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.Messenging;
using ConnectTheDotsCP.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ConnectTheDotsCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class ConnectTheDotsGameContainer : BasicGameContainer<ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo>
    {
        public ConnectTheDotsGameContainer(
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
        //needs to be public so blazor can work with it.
        public Dictionary<int, SquareInfo>? SquareList { get; set; }
        public Dictionary<int, LineInfo>? LineList { get; set; }
        public Dictionary<int, DotInfo>? DotList { get; set; }
        public LineInfo PreviousLine { get; set; } = new LineInfo();
        public DotInfo PreviousDot { get; set; } = new DotInfo();
        public Func<int, Task>? MakeMoveAsync { get; set; }
    }
}
