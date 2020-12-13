using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using XactikaCP.Cards;
namespace XactikaCP.Data
{
    [SingletonGame]
    public class XactikaDetailClass : IGameInfo, ICardInfo<XactikaCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Xactika";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<XactikaCardInformation>.CardsToPassOut => 8;

        CustomBasicList<int> ICardInfo<XactikaCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<XactikaCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<XactikaCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<XactikaCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<XactikaCardInformation>.PassOutAll => false;

        bool ICardInfo<XactikaCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<XactikaCardInformation>.NoPass => false;

        bool ICardInfo<XactikaCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<XactikaCardInformation> ICardInfo<XactikaCardInformation>.DummyHand { get; set; } = new DeckRegularDict<XactikaCardInformation>();

        bool ICardInfo<XactikaCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<XactikaCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => false;

        bool ITrickData.HasTrump => false;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => false;

        CustomBasicList<int> ICardInfo<XactikaCardInformation>.DiscardExcludeList(IListShuffler<XactikaCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}