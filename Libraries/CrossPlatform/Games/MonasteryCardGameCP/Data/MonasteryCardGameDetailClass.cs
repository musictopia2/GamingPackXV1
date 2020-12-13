using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace MonasteryCardGameCP.Data
{
    [SingletonGame]
    public class MonasteryCardGameDetailClass : IGameInfo, ICardInfo<MonasteryCardInfo>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;

        string IGameInfo.GameName => "Monastery Card Game";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<MonasteryCardInfo>.CardsToPassOut => 9;

        CustomBasicList<int> ICardInfo<MonasteryCardInfo>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<MonasteryCardInfo>.AddToDiscardAtBeginning => true;

        bool ICardInfo<MonasteryCardInfo>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<MonasteryCardInfo>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<MonasteryCardInfo>.PassOutAll => false;

        bool ICardInfo<MonasteryCardInfo>.PlayerGetsCards => true;

        bool ICardInfo<MonasteryCardInfo>.NoPass => false;

        bool ICardInfo<MonasteryCardInfo>.NeedsDummyHand => false;

        DeckRegularDict<MonasteryCardInfo> ICardInfo<MonasteryCardInfo>.DummyHand { get; set; } = new DeckRegularDict<MonasteryCardInfo>();

        bool ICardInfo<MonasteryCardInfo>.HasDrawAnimation => true;

        bool ICardInfo<MonasteryCardInfo>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<MonasteryCardInfo>.DiscardExcludeList(IListShuffler<MonasteryCardInfo> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}