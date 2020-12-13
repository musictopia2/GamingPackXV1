using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using YahtzeeHandsDownCP.Cards;
namespace YahtzeeHandsDownCP.Data
{
    [SingletonGame]
    public class YahtzeeHandsDownDetailClass : IGameInfo, ICardInfo<YahtzeeHandsDownCardInformation>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;

        string IGameInfo.GameName => "Yahtzee Hands Down";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 6;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<YahtzeeHandsDownCardInformation>.CardsToPassOut => 5;

        CustomBasicList<int> ICardInfo<YahtzeeHandsDownCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<YahtzeeHandsDownCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<YahtzeeHandsDownCardInformation>.ReshuffleAllCardsFromDiscard => true;

        bool ICardInfo<YahtzeeHandsDownCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<YahtzeeHandsDownCardInformation>.PassOutAll => false;

        bool ICardInfo<YahtzeeHandsDownCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<YahtzeeHandsDownCardInformation>.NoPass => false;

        bool ICardInfo<YahtzeeHandsDownCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<YahtzeeHandsDownCardInformation> ICardInfo<YahtzeeHandsDownCardInformation>.DummyHand { get; set; } = new DeckRegularDict<YahtzeeHandsDownCardInformation>();

        bool ICardInfo<YahtzeeHandsDownCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<YahtzeeHandsDownCardInformation>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<YahtzeeHandsDownCardInformation>.DiscardExcludeList(IListShuffler<YahtzeeHandsDownCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}