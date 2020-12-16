using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using RackoCP.Cards;
namespace RackoCP.Data
{
    [SingletonGame]
    public class RackoDetailClass : IGameInfo, ICardInfo<RackoCardInformation>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false; //can no longer have extra computer players because there was some bugs.  decided for now, not worth fixing.  not as important.

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Racko";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Portrait;

        int ICardInfo<RackoCardInformation>.CardsToPassOut => 10;

        CustomBasicList<int> ICardInfo<RackoCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<RackoCardInformation>.AddToDiscardAtBeginning => true;

        bool ICardInfo<RackoCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<RackoCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<RackoCardInformation>.PassOutAll => false;

        bool ICardInfo<RackoCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<RackoCardInformation>.NoPass => false;

        bool ICardInfo<RackoCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<RackoCardInformation> ICardInfo<RackoCardInformation>.DummyHand { get; set; } = new DeckRegularDict<RackoCardInformation>();

        bool ICardInfo<RackoCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<RackoCardInformation>.CanSortCardsToBeginWith => false;

        CustomBasicList<int> ICardInfo<RackoCardInformation>.DiscardExcludeList(IListShuffler<RackoCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}