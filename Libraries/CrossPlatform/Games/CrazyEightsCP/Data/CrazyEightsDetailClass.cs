using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace CrazyEightsCP.Data
{
    [SingletonGame]
    public class CrazyEightsDetailClass : IGameInfo, ICardInfo<RegularSimpleCard>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

        bool IGameInfo.CanHaveExtraComputerPlayers => true;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Crazy Eights";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<RegularSimpleCard>.CardsToPassOut => 7;

        CustomBasicList<int> ICardInfo<RegularSimpleCard>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<RegularSimpleCard>.AddToDiscardAtBeginning => true;

        bool ICardInfo<RegularSimpleCard>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<RegularSimpleCard>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<RegularSimpleCard>.PassOutAll => false;

        bool ICardInfo<RegularSimpleCard>.PlayerGetsCards => true;

        bool ICardInfo<RegularSimpleCard>.NoPass => false;

        bool ICardInfo<RegularSimpleCard>.NeedsDummyHand => false;

        DeckRegularDict<RegularSimpleCard> ICardInfo<RegularSimpleCard>.DummyHand { get; set; } = new DeckRegularDict<RegularSimpleCard>();

        bool ICardInfo<RegularSimpleCard>.HasDrawAnimation => true;

        bool ICardInfo<RegularSimpleCard>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<RegularSimpleCard>.DiscardExcludeList(IListShuffler<RegularSimpleCard> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}