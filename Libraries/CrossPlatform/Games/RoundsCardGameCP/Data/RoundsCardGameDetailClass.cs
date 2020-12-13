using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using RoundsCardGameCP.Cards;
namespace RoundsCardGameCP.Data
{
    [SingletonGame]
    public class RoundsCardGameDetailClass : IGameInfo, ICardInfo<RoundsCardGameCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Rounds Card Game";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 2;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<RoundsCardGameCardInformation>.CardsToPassOut => 9;

        CustomBasicList<int> ICardInfo<RoundsCardGameCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<RoundsCardGameCardInformation>.AddToDiscardAtBeginning => true;

        bool ICardInfo<RoundsCardGameCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<RoundsCardGameCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<RoundsCardGameCardInformation>.PassOutAll => false;

        bool ICardInfo<RoundsCardGameCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<RoundsCardGameCardInformation>.NoPass => false;

        bool ICardInfo<RoundsCardGameCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<RoundsCardGameCardInformation> ICardInfo<RoundsCardGameCardInformation>.DummyHand { get; set; } = new DeckRegularDict<RoundsCardGameCardInformation>();

        bool ICardInfo<RoundsCardGameCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<RoundsCardGameCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => true;

        bool ITrickData.HasTrump => false;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => false;

        CustomBasicList<int> ICardInfo<RoundsCardGameCardInformation>.DiscardExcludeList(IListShuffler<RoundsCardGameCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}