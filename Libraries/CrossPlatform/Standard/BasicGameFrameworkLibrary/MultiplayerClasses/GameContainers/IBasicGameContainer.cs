using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers
{
    public interface IBasicGameContainer
    {
        IEventAggregator? Aggregator { get; }
        BasicData? BasicData { get; }
        IMessageChecker? Check { get; }
        CommandContainer? Command { get; }
        Func<Task>? ContinueTurnAsync { get; set; }
        IAsyncDelayer? Delay { get; }
        Func<Task>? EndTurnAsync { get; set; }
        IGameInfo? GameInfo { get; }
        INetworkMessages? Network { get; }
        RandomGenerator? Random { get; }
        IGamePackageResolver? Resolver { get; }
        Func<Task>? ShowWinAsync { get; set; }
        TestOptions Test { get; }
        int WhoTurn { get; set; }
    }
}