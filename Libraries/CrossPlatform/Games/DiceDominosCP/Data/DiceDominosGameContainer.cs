using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace DiceDominosCP.Data
{
    [SingletonGame]
    public class DiceDominosGameContainer : BasicGameContainer<DiceDominosPlayerItem, DiceDominosSaveInfo>
    {
        public DiceDominosGameContainer(BasicData basicData,
            TestOptions test,
            IGameInfo gameInfo,
            IAsyncDelayer delay,
            IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            RandomGenerator random,
            DominosBasicShuffler<SimpleDominoInfo> dominosShuffler
            ) : base(basicData, test, gameInfo, delay, aggregator, command, resolver, random)
        {
            DominosShuffler = dominosShuffler;
        }
        public DominosBasicShuffler<SimpleDominoInfo> DominosShuffler { get; } //hopefully this simple.
        internal Func<SimpleDominoInfo, Task>? DominoClickedAsync { get; set; } //since i am using a standard control, maybe this will work (?)
    }
}