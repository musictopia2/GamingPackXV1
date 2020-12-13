using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace FourSuitRummyCP.Data
{
    [SingletonGame]
    public class FourSuitRummyDetailClass : IGameInfo, ICardInfo<RegularRummyCard>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;

        string IGameInfo.GameName => "Four Suit Rummy";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 2;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<RegularRummyCard>.CardsToPassOut => 12;

        CustomBasicList<int> ICardInfo<RegularRummyCard>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<RegularRummyCard>.AddToDiscardAtBeginning => true;

        bool ICardInfo<RegularRummyCard>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<RegularRummyCard>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<RegularRummyCard>.PassOutAll => false;

        bool ICardInfo<RegularRummyCard>.PlayerGetsCards => true;

        bool ICardInfo<RegularRummyCard>.NoPass => false;

        bool ICardInfo<RegularRummyCard>.NeedsDummyHand => false;

        DeckRegularDict<RegularRummyCard> ICardInfo<RegularRummyCard>.DummyHand { get; set; } = new DeckRegularDict<RegularRummyCard>();

        bool ICardInfo<RegularRummyCard>.HasDrawAnimation => true;

        bool ICardInfo<RegularRummyCard>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<RegularRummyCard>.DiscardExcludeList(IListShuffler<RegularRummyCard> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}