using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Messenging;
namespace CheckersCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class CheckersGameContainer : BasicGameContainer<CheckersPlayerItem, CheckersSaveInfo>
    {
        public CheckersGameContainer(
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
            Animates.LongestTravelTime = 200;
            CurrentCrowned = false; //i think.
        }
        public AnimateBasicGameBoard Animates;
        public bool CurrentCrowned;
        public CustomBasicList<MoveInfo> CompleteMoveList { get; set; } = new CustomBasicList<MoveInfo>();
        public CustomBasicList<MoveInfo> CurrentMoveList { get; set; } = new CustomBasicList<MoveInfo>();
        public CustomBasicList<SpaceCP>? SpaceList;
        public bool CanUpdate { get; set; } = true;

    }
}