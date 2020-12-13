using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using SnagCardGameCP.Cards;
namespace SnagCardGameCP.Data
{
    [SingletonGame]
    public class SnagCardGameDetailClass : IGameInfo, ICardInfo<SnagCardGameCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Snag Card Game";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 2;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Portrait;

        int ICardInfo<SnagCardGameCardInformation>.CardsToPassOut => 5;

        CustomBasicList<int> ICardInfo<SnagCardGameCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<SnagCardGameCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<SnagCardGameCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<SnagCardGameCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<SnagCardGameCardInformation>.PassOutAll => false;

        bool ICardInfo<SnagCardGameCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<SnagCardGameCardInformation>.NoPass => false;

        bool ICardInfo<SnagCardGameCardInformation>.NeedsDummyHand => true;

        DeckRegularDict<SnagCardGameCardInformation> ICardInfo<SnagCardGameCardInformation>.DummyHand { get; set; } = new DeckRegularDict<SnagCardGameCardInformation>();

        bool ICardInfo<SnagCardGameCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<SnagCardGameCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => true;

        bool ITrickData.HasTrump => false;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => true;

        CustomBasicList<int> ICardInfo<SnagCardGameCardInformation>.DiscardExcludeList(IListShuffler<SnagCardGameCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}