using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using MonopolyCardGameCP.Cards;
namespace MonopolyCardGameCP.Data
{
    [SingletonGame]
    public class MonopolyCardGameDetailClass : IGameInfo, ICardInfo<MonopolyCardGameCardInformation>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;

        string IGameInfo.GameName => "Monopoly Card Game";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<MonopolyCardGameCardInformation>.CardsToPassOut => 10;

        CustomBasicList<int> ICardInfo<MonopolyCardGameCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<MonopolyCardGameCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<MonopolyCardGameCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<MonopolyCardGameCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<MonopolyCardGameCardInformation>.PassOutAll => false;

        bool ICardInfo<MonopolyCardGameCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<MonopolyCardGameCardInformation>.NoPass => false;

        bool ICardInfo<MonopolyCardGameCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<MonopolyCardGameCardInformation> ICardInfo<MonopolyCardGameCardInformation>.DummyHand { get; set; } = new DeckRegularDict<MonopolyCardGameCardInformation>();

        bool ICardInfo<MonopolyCardGameCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<MonopolyCardGameCardInformation>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<MonopolyCardGameCardInformation>.DiscardExcludeList(IListShuffler<MonopolyCardGameCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}