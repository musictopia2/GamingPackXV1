using AccordianSolitaireCP.Data;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace AccordianSolitaireCP.Logic
{
    [SingletonGame]
    public class AccordianSolitaireMainGameClass : RegularDeckOfCardsGameClass<AccordianSolitaireCardInfo>, IAggregatorContainer
    {
        private readonly ISaveSinglePlayerClass _thisState;
        private readonly CommandContainer _command;
        internal AccordianSolitaireSaveInfo _saveRoot;
        internal bool GameGoing { get; set; }
        public AccordianSolitaireMainGameClass(ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IGamePackageResolver container,
            CommandContainer command
            )
        {
            _thisState = thisState;
            Aggregator = aggregator;
            _command = command;
            _saveRoot = container.ReplaceObject<AccordianSolitaireSaveInfo>(); //can't create new one.  because if doing that, then anything that needs it won't have it.
            _saveRoot.LoadMod(Aggregator);
        }
        private GameBoard? _board;
        public async Task NewGameAsync(DeckObservablePile<AccordianSolitaireCardInfo> deck, GameBoard board)
        {
            GameGoing = true;
            _board = board;
            await base.NewGameAsync(deck);
            _command.UpdateAll();
        }
        public override Task NewGameAsync(DeckObservablePile<AccordianSolitaireCardInfo> deck)
        {
            throw new BasicBlankException("Needs to use new method for newgame to send in the gameboard");
        }
        public IEventAggregator Aggregator { get; }
        public override async Task<bool> CanOpenSavedSinglePlayerGameAsync()
        {
            return await _thisState.CanOpenSavedSinglePlayerGameAsync();
        }
        public override async Task OpenSavedGameAsync()
        {
            DeckList.OrderedObjects(); //i think
            _saveRoot = await _thisState.RetrieveSinglePlayerGameAsync<AccordianSolitaireSaveInfo>();
            if (_saveRoot.DeckList.Count > 0)
            {
                var newList = _saveRoot.DeckList.GetNewObjectListFromDeckList(DeckList);
                DeckPile!.OriginalList(newList);
            }
            _saveRoot.LoadMod(Aggregator);
            _board!.ReloadSavedGame(_saveRoot);
        }
        private bool _isBusy;
        public async Task SaveStateAsync()
        {
            if (_isBusy)
            {
                return;
            }
            _isBusy = true;
            _saveRoot.DeckList = DeckPile!.GetCardIntegers();
            _board!.SaveGame();
            await _thisState.SaveSimpleSinglePlayerGameAsync(_saveRoot); //i think
            _isBusy = false;
        }
        public async Task ShowWinAsync()
        {
            ToastPlatform.ShowSuccess("Congratulations, you won");
            await Task.Delay(2000);
            GameGoing = false;
            await this.SendGameOverAsync();
        }
        protected override void AfterShuffle()
        {
            _saveRoot.Score = 1;
            var thisList = DeckList.ToRegularDeckDict();
            thisList.MakeAllObjectsKnown();
            _board!.NewGame(thisList, _saveRoot);
        }
    }
}