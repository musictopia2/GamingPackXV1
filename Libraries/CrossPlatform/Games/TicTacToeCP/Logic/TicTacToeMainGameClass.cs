using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using TicTacToeCP.Data;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace TicTacToeCP.Logic
{
    [SingletonGame]
    public class TicTacToeMainGameClass : BasicGameClass<TicTacToePlayerItem, TicTacToeSaveInfo>, IMoveNM
    {
        public TicTacToeMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            TicTacToeVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            RandomGenerator rs,
            TicTacToeGameContainer gameContainer
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, gameContainer)
        {
            _command = command;
        }

        private readonly CommandContainer _command;

        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            return Task.CompletedTask;
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
            {
                return;
            }
            IsLoaded = true;
        }
        protected override async Task ComputerTurnAsync()
        {
            await Task.CompletedTask;
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            PlayerList.First().Piece = EnumSpaceType.X;
            PlayerList.Last().Piece = EnumSpaceType.O;
            SaveRoot!.GameBoard.Clear(); //i think here too.
            await FinishUpAsync(isBeginning);
        }
        public override async Task StartNewTurnAsync()
        {
            PrepStartTurn();
            await ContinueTurnAsync(); //most of the time, continue turn.  can change to what is needed
        }
        public override async Task EndTurnAsync()
        {
            _command.ManuelFinish = true;
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        public async Task MakeMoveAsync(SpaceInfoCP space)
        {
            SingleInfo = SaveRoot!.PlayerList.GetWhoPlayer();
            space.Status = SingleInfo.Piece;
            WinInfo thisWin = SaveRoot.GameBoard.GetWin();
            if (BasicData!.MultiPlayer == true && SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
                await Network!.SendMoveAsync(space);
            if (thisWin.WinList.Count > 0)
            {
                await ShowWinAsync();
                return;
            }
            if (thisWin.IsDraw == true)
            {
                await ShowTieAsync();
                return;
            }
            await EndTurnAsync();
        }
        async Task IMoveNM.MoveReceivedAsync(string data)
        {
            SpaceInfoCP tempMove = await js.DeserializeObjectAsync<SpaceInfoCP>(data);
            SpaceInfoCP realMove = SaveRoot!.GameBoard[tempMove.Vector]; //i think
            await MakeMoveAsync(realMove);
        }
    }
}