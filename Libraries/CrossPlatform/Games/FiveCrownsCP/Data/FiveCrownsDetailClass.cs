using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using FiveCrownsCP.Cards;
using FiveCrownsCP.Logic;
namespace FiveCrownsCP.Data
{

    [SingletonGame]
    public class FiveCrownsDetailClass : IGameInfo, ICardInfo<FiveCrownsCardInformation>
    {
        private readonly FiveCrownsDelegates _delegates;
        public FiveCrownsDetailClass(FiveCrownsDelegates delegates)
        {
            _delegates = delegates;
        }
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;

        string IGameInfo.GameName => "Five Crowns";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 7;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<FiveCrownsCardInformation>.CardsToPassOut => _delegates.CardsToPassOut!.Invoke();

        CustomBasicList<int> ICardInfo<FiveCrownsCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<FiveCrownsCardInformation>.AddToDiscardAtBeginning => true;

        bool ICardInfo<FiveCrownsCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<FiveCrownsCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<FiveCrownsCardInformation>.PassOutAll => false;

        bool ICardInfo<FiveCrownsCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<FiveCrownsCardInformation>.NoPass => false;

        bool ICardInfo<FiveCrownsCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<FiveCrownsCardInformation> ICardInfo<FiveCrownsCardInformation>.DummyHand { get; set; } = new DeckRegularDict<FiveCrownsCardInformation>();

        bool ICardInfo<FiveCrownsCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<FiveCrownsCardInformation>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<FiveCrownsCardInformation>.DiscardExcludeList(IListShuffler<FiveCrownsCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}