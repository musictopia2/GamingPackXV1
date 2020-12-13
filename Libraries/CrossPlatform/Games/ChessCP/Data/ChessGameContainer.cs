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
namespace ChessCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class ChessGameContainer : BasicGameContainer<ChessPlayerItem, ChessSaveInfo>
    {
        public ChessGameContainer(
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
            CurrentPiece = EnumPieceType.None;
        }

        public AnimateBasicGameBoard Animates;
        internal CustomBasicList<MoveInfo> CompleteMoveList { get; set; } = new CustomBasicList<MoveInfo>();
        public CustomBasicList<MoveInfo> CurrentMoveList { get; set; } = new CustomBasicList<MoveInfo>(); //needs to be public so blazor ui can use it.
        public CustomBasicList<SpaceCP>? SpaceList;
        public EnumPieceType CurrentPiece { get; set; } //has to be public so blazor can use it.
    }
}