using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using HorseshoeCardGameCP.Cards;
namespace HorseshoeCardGameCP.Data
{
    [SingletonGame]
    public class HorseshoeCardGameDetailClass : IGameInfo, ICardInfo<HorseshoeCardGameCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Horseshoe";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 2;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<HorseshoeCardGameCardInformation>.CardsToPassOut => 6;

        CustomBasicList<int> ICardInfo<HorseshoeCardGameCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<HorseshoeCardGameCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<HorseshoeCardGameCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<HorseshoeCardGameCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<HorseshoeCardGameCardInformation>.PassOutAll => false;

        bool ICardInfo<HorseshoeCardGameCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<HorseshoeCardGameCardInformation>.NoPass => false;

        bool ICardInfo<HorseshoeCardGameCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<HorseshoeCardGameCardInformation> ICardInfo<HorseshoeCardGameCardInformation>.DummyHand { get; set; } = new DeckRegularDict<HorseshoeCardGameCardInformation>();

        bool ICardInfo<HorseshoeCardGameCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<HorseshoeCardGameCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => false;

        bool ITrickData.HasTrump => false;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => true;

        CustomBasicList<int> ICardInfo<HorseshoeCardGameCardInformation>.DiscardExcludeList(IListShuffler<HorseshoeCardGameCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}