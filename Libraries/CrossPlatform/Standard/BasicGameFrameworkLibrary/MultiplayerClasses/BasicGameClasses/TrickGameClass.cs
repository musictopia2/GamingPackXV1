using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses
{
    public abstract class TrickGameClass<SU, T, P, SA> : CardGameClass<T, P, SA>, ITrickNM
        where SU : struct, Enum
        where T : class, ITrickCard<SU>, new()
        where P : class, IPlayerTrick<SU, T>, new()
        where SA : BasicSavedTrickGamesClass<SU, T, P>, new()
    {
        private readonly ITrickData _trickData;
        private readonly ITrickPlay _trickPlay;
        private readonly ITrickCardGamesData<T, SU> _model;
        private readonly TrickGameContainer<T, P, SA, SU> _gameContainer;
        public TrickGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            ITrickCardGamesData<T, SU> currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            ICardInfo<T> cardInfo,
            CommandContainer command,
            TrickGameContainer<T, P, SA, SU> gameContainer,
            ITrickData trickData,
            ITrickPlay trickPlay
            ) : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer)
        {
            _trickData = trickData;
            _trickPlay = trickPlay;
            _model = currentMod;
            _gameContainer = gameContainer;
            _gameContainer.CardClickedAsync = CardClickedAsync;
            _gameContainer.ContinueTrickAsync = ContinueTrickAsync;
            _gameContainer.EndTrickAsync = EndTrickAsync;
        }
        public virtual bool CanEnableTrickAreas => true; //has to be overridable because some trick games, you can't enable in some situations like when bidding.
        protected virtual void LoadVM()
        {
            var thisA = _gameContainer.Resolver.Resolve<IAdvancedTrickProcesses>(); //hopefully this simple (?)
            thisA.FirstLoad(); //try when loading vm.
            SaveRoot!.LoadTrickVM(_model);
        }

        private bool HeartsValidMove(int deck)
        {
            var heartSave = (ITrickStatusSavedClass)SaveRoot!;
            var thisList = SaveRoot!.TrickList;
            var thisCard = _gameContainer.DeckList!.GetSpecificItem(deck);
            EnumSuitList cardSuit = thisCard.GetSuit.GetRegularSuit();
            if (thisList.Count == 0)
            {
                if (heartSave.TrickStatus == EnumTrickStatus.FirstTrick)
                {
                    var tempCard = SingleInfo!.MainHandList.OrderBy(Items => Items.GetSuit).ThenBy(Items => Items.ReadMainValue).Take(1).Single();
                    if (tempCard.Deck == deck)
                    {
                        return true;
                    }
                    return false;
                }
                if (heartSave.TrickStatus == EnumTrickStatus.SuitBroken)
                {
                    return true;
                }
                if (cardSuit == EnumSuitList.Hearts)
                {
                    return !SingleInfo!.MainHandList.Any(Items => Items.GetSuit.GetRegularSuit() != EnumSuitList.Hearts);
                }
                return true;
            }
            var leadCard = thisList.First();
            if (leadCard.GetSuit.Equals(thisCard.GetSuit))
            {
                return true;
            }
            DeckRegularDict<T> tempList;
            if (_trickData!.HasDummy == true)
            {
                var temps = MainContainer.Resolve<ITrickDummyHand<SU, T>>(); //if you don't have it, then will raise an error.
                tempList = temps.GetCurrentHandList();
            }
            else
            {
                tempList = SingleInfo!.MainHandList;
            }
            if (tempList.Any(Items => Items.GetSuit.Equals(leadCard.GetSuit)))
            {
                return false;
            }
            if (heartSave.TrickStatus == EnumTrickStatus.FirstTrick)
            {
                if (cardSuit == EnumSuitList.Hearts && tempList.Any(Items => Items.GetSuit.GetRegularSuit() != EnumSuitList.Hearts))
                {
                    return false;
                }
                if (cardSuit == EnumSuitList.Spades && thisCard.ReadMainValue == 12)
                {
                    return false;
                }
            }
            return true;
        }
        private bool SpadesValidMove(int deck)
        {
            var spadeSave = (ITrickStatusSavedClass)SaveRoot!;
            var thisList = SaveRoot!.TrickList;
            var thisCard = _gameContainer.DeckList!.GetSpecificItem(deck);
            EnumSuitList cardSuit = thisCard.GetSuit.GetRegularSuit();
            if (thisList.Count == 0)
            {
                if (spadeSave.TrickStatus == EnumTrickStatus.SuitBroken)
                {
                    return true;
                }
                if (cardSuit == EnumSuitList.Spades)
                {
                    return !SingleInfo!.MainHandList.Any(Items => Items.GetSuit.GetRegularSuit() != EnumSuitList.Spades);
                }
                return true;
            }
            var leadCard = thisList.First();
            if (leadCard.GetSuit.Equals(thisCard.GetSuit))
            {
                return true;
            }
            if (SingleInfo!.MainHandList.Any(Items => Items.GetSuit.Equals(leadCard.GetSuit)))
            {
                return false;
            }
            if (spadeSave.TrickStatus == EnumTrickStatus.FirstTrick)
            {
                if (cardSuit == EnumSuitList.Spades && SingleInfo.MainHandList.Any(Items => Items.GetSuit.GetRegularSuit() != EnumSuitList.Spades))
                {
                    return false;
                }
            }
            return true;
        }
        public virtual bool IsValidMove(int deck) //needs to be public so computer ai class can call into it.
        {
            if (_trickData!.TrickStyle == EnumTrickStyle.Hearts)
            {
                return HeartsValidMove(deck);
            }
            if (_trickData.TrickStyle == EnumTrickStyle.Spades)
            {
                return SpadesValidMove(deck);
            }
            var thisList = SaveRoot!.TrickList;
            if (thisList.Count == 0 && _trickData.FirstPlayerAnySuit == true)
            {
                return true;
            }
            var leadCard = thisList.First();
            var thisCard = _gameContainer.DeckList!.GetSpecificItem(deck);
            if (_trickData.FollowSuit == true && thisCard.GetSuit.Equals(leadCard.GetSuit))
            {
                return true;
            }
            DeckRegularDict<T> currentHand;
            if (_trickData.HasDummy)
            {
                ITrickDummyHand<SU, T> temps = MainContainer.Resolve<ITrickDummyHand<SU, T>>();
                currentHand = temps.GetCurrentHandList();
            }
            else
            {
                currentHand = SingleInfo!.MainHandList;
            }
            if (_trickData.MustFollow == true)
            {
                if (currentHand.Any(Items => Items.GetSuit.Equals(leadCard.GetSuit)))
                {
                    return false; //because you have to follow suit
                }
            }
            if (_trickData.HasTrump == true)
            {
                if (thisCard.GetSuit.Equals(SaveRoot.TrumpSuit))
                {
                    return true;
                }
            }
            if (_trickData.MustPlayTrump == true)
            {
                if (currentHand.Any(Items => Items.GetSuit.Equals(SaveRoot.TrumpSuit)))
                {
                    return false; //because the setting says you have to play trump
                }
            }
            return true;
        }
        protected virtual async Task PlayCardAsync(int deck) //most of the time this simple.  could rethink if necessary.
        {
            if (_trickData!.HasDummy == true)
            {
                ITrickDummyHand<SU, T> temps = MainContainer.Resolve<ITrickDummyHand<SU, T>>();
                temps.RemoveCard(deck);
            }
            else
            {
                SingleInfo!.MainHandList.RemoveObjectByDeck(deck);
            }
            await _trickPlay!.PlayCardAsync(deck);
        }
        protected string PlayErrorMessage { get; set; } = "Illegal Move";
        protected virtual int PossibleOtherSelected(int firstChosen, out string message)
        {
            message = "";
            return firstChosen;
        }
        public async Task CardClickedAsync() //this is a card being clicked on.
        {
            int decks;

            if (_model.PlayerHand1!.AutoSelect == EnumHandAutoType.SelectAsMany)
            {
                if (_model.PlayerHand1.HowManySelectedObjects > 1)
                {
                    ToastPlatform.ShowError("Can you only choose one card at a time to play");
                    await ShowHumanCanPlayAsync();
                    return;
                }
            }
            if (_trickData!.HasDummy == true)
            {
                ITrickDummyHand<SU, T> temps = MainContainer.Resolve<ITrickDummyHand<SU, T>>();
                decks = temps.CardSelected();
            }
            else
            {
                decks = _model.PlayerHand1.ObjectSelected();
            }
            decks = PossibleOtherSelected(decks, out string message);
            if (message != "")
            {
                ToastPlatform.ShowError(message);
                await ShowHumanCanPlayAsync();
                return;
            }
            if (decks == 0) //if you did not choose a card, its already handled if no message is sent.
            {
                ToastPlatform.ShowError("Must choose a card to play");
                await ShowHumanCanPlayAsync();
                return;
            }
            if (IsValidMove(decks) == false)
            {
                ToastPlatform.ShowError(PlayErrorMessage);
                PlayErrorMessage = "Illegal Move"; //to set back.  will accomodate games like sixty six and maybe pinacle.
                await ShowHumanCanPlayAsync();
                return;
            }
            if (BasicData!.MultiPlayer == true)
            {
                await Network!.SendAllAsync("trickplay", decks); //i think
            }
            await PlayCardAsync(decks);
        }
        protected virtual bool CanEndTurnToContinueTrick => true;
        public virtual async Task ContinueTrickAsync()
        {
            if (CanEndTurnToContinueTrick == true)
            {
                await EndTurnAsync(); //usually will end turn but can have exceptions.
            }
            else
            {
                await ContinueTurnAsync();
            }
        }
        public abstract Task EndTrickAsync();
        async Task ITrickNM.TrickPlayReceivedAsync(int deck)
        {
            await PlayCardAsync(deck);
        }
    }
}