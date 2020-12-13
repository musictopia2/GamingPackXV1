using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Phase10CP.Cards;
namespace Phase10CP.Data
{
    [SingletonGame]
    public class Phase10DetailClass : IGameInfo, ICardInfo<Phase10CardInformation>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;

        string IGameInfo.GameName => "Phase 10";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 7;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<Phase10CardInformation>.CardsToPassOut => 10;

        CustomBasicList<int> ICardInfo<Phase10CardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<Phase10CardInformation>.AddToDiscardAtBeginning => true;

        bool ICardInfo<Phase10CardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<Phase10CardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<Phase10CardInformation>.PassOutAll => false;

        bool ICardInfo<Phase10CardInformation>.PlayerGetsCards => true;

        bool ICardInfo<Phase10CardInformation>.NoPass => false;

        bool ICardInfo<Phase10CardInformation>.NeedsDummyHand => false;

        DeckRegularDict<Phase10CardInformation> ICardInfo<Phase10CardInformation>.DummyHand { get; set; } = new DeckRegularDict<Phase10CardInformation>();

        bool ICardInfo<Phase10CardInformation>.HasDrawAnimation => true;

        bool ICardInfo<Phase10CardInformation>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<Phase10CardInformation>.DiscardExcludeList(IListShuffler<Phase10CardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}