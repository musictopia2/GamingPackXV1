using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using RookCP.Cards;
namespace RookCP.Data
{
    [SingletonGame]
    public class RookDetailClass : IGameInfo, ICardInfo<RookCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Rook";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 3;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<RookCardInformation>.CardsToPassOut => 12;

        CustomBasicList<int> ICardInfo<RookCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<RookCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<RookCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<RookCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<RookCardInformation>.PassOutAll => false;

        bool ICardInfo<RookCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<RookCardInformation>.NoPass => false;

        bool ICardInfo<RookCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<RookCardInformation> ICardInfo<RookCardInformation>.DummyHand { get; set; } = new DeckRegularDict<RookCardInformation>();

        bool ICardInfo<RookCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<RookCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => true;

        bool ITrickData.HasTrump => true;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => true;

        CustomBasicList<int> ICardInfo<RookCardInformation>.DiscardExcludeList(IListShuffler<RookCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}