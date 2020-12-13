using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using ThreeLetterFunCP.Data;
using ThreeLetterFunCP.Logic;
namespace ThreeLetterFunCP.ViewModels
{
    [InstanceGame]
    public class ThreeLetterFunMainViewModel : BasicMultiplayerMainVM
    {
        public readonly ThreeLetterFunMainGameClass MainGame;
        private readonly BasicData _basicData;
        private readonly GiveUpClass _giveUp;
        public readonly GameBoard GameBoard;
        private readonly GlobalHelpers _global;
        public ThreeLetterFunVMData GameData;
        public ThreeLetterFunMainViewModel(CommandContainer commandContainer,
            ThreeLetterFunMainGameClass mainGame,
            ThreeLetterFunVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            GiveUpClass giveUp,
            GameBoard board,
            GlobalHelpers global
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            MainGame = mainGame;
            _basicData = basicData;
            _giveUp = giveUp;
            GameBoard = board;
            _global = global;
            GameData = viewModel;

        }
        [Command(EnumCommandCategory.Game)]
        public async Task PlayAsync()
        {
            _global.PauseContinueTimer();
            if (_basicData.MultiPlayer == true && MainGame!.Check!.IsEnabled == true)
            {
                await UIPlatform.ShowMessageAsync("Should not have enabled the network helpers since you had to take your turn.");
                return;
            }
            var thisCard = GameBoard.GetCompletedCard();
            if (thisCard == null)
            {
                _global.PauseContinueTimer();
                await UIPlatform.ShowMessageAsync("You must pick the tiles before playing");
                return;
            }
            if (thisCard.IsValidWord() == false)
            {
                var thisWord = thisCard.GetWord();
                if (MainGame.SaveRoot!.Level == EnumLevel.Easy)
                {
                    GameBoard.UnDo();
                    await UIPlatform.ShowMessageAsync($"{thisWord} is not a word or is too hard. Please try again");
                    _global.PauseContinueTimer();
                    return;
                }
                if (_basicData.MultiPlayer == false)
                {
                    await UIPlatform.ShowMessageAsync($"{thisWord} does not exist.  Therefore; its going to the next one");
                    await _giveUp.SelfGiveUpAsync(true);
                    return;
                }
                await UIPlatform.ShowMessageAsync($"{thisWord} does not exist.  Therefore; waiting for other players to decide if they have a word");
                await _giveUp.SelfGiveUpAsync(true);
                return;
            }
            if (_basicData.MultiPlayer == true)
            {
                MainGame.SingleInfo = MainGame.PlayerList!.GetSelf();
                TempWord thisWord = new TempWord();
                thisWord.Player = MainGame.SingleInfo.Id;
                thisWord.CardUsed = thisCard.Deck;
                thisWord.TileList = MainGame.SingleInfo.TileList;
                if (thisWord.TileList.Count == 0)
                {
                    throw new BasicBlankException("Must have tiles to form a word to send");
                }
                MainGame.SingleInfo.TimeToGetWord = (int)_global.Stops!.TimeTaken();
                _global.Stops.ManualStop(false);
                thisWord.TimeToGetWord = MainGame.SingleInfo.TimeToGetWord;
                if (MainGame.SingleInfo.TimeToGetWord == 0)
                {
                    throw new BasicBlankException("Time Taken Cannot Be 0");
                }
                await MainGame.Network!.SendAllAsync("playword", thisWord);
                MainGame.SaveRoot!.PlayOrder.WhoTurn = MainGame.SingleInfo.Id;
            }
            await MainGame!.PlayWordAsync(thisCard.Deck);
        }
        [Command(EnumCommandCategory.Game)]
        public async Task GiveUpAsync()
        {
            await _giveUp.SelfGiveUpAsync(true);
        }
        [Command(EnumCommandCategory.Game)]
        public void TakeBack()
        {
            GameBoard.UnDo();
        }

        private string _playerWon = "";
        [VM]
        public string PlayerWon
        {
            get { return _playerWon; }
            set
            {
                if (SetProperty(ref _playerWon, value))
                {
                    CalculateVisible();
                }
            }
        }
        private ThreeLetterFunCardData? _currentCard;
        [VM]
        public ThreeLetterFunCardData? CurrentCard
        {
            get { return _currentCard; }
            set
            {
                if (SetProperty(ref _currentCard, value))
                {
                    CalculateVisible();
                }
            }
        }
        private void CalculateVisible()
        {
            if (CurrentCard != null)
            {
                GameBoard.Visible = false;
                return;
            }
            if (PlayerWon != "")
            {
                GameBoard.Visible = false;
                return;
            }
            GameBoard.Visible = true;
        }
    }
}