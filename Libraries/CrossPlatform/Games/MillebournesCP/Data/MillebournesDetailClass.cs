using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using MillebournesCP.Cards;
namespace MillebournesCP.Data
{
    [SingletonGame]
    public class MillebournesDetailClass : IGameInfo, ICardInfo<MillebournesCardInformation>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => true;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Millebournes";

        int IGameInfo.NoPlayers => 5;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 6;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<MillebournesCardInformation>.CardsToPassOut => 6;

        CustomBasicList<int> ICardInfo<MillebournesCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<MillebournesCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<MillebournesCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<MillebournesCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<MillebournesCardInformation>.PassOutAll => false;

        bool ICardInfo<MillebournesCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<MillebournesCardInformation>.NoPass => false;

        bool ICardInfo<MillebournesCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<MillebournesCardInformation> ICardInfo<MillebournesCardInformation>.DummyHand { get; set; } = new DeckRegularDict<MillebournesCardInformation>();

        bool ICardInfo<MillebournesCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<MillebournesCardInformation>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<MillebournesCardInformation>.DiscardExcludeList(IListShuffler<MillebournesCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}