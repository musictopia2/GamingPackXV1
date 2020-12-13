using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using DutchBlitzCP.Cards;
namespace DutchBlitzCP.Data
{
    [SingletonGame]
    public class DutchBlitzDetailClass : IGameInfo, ICardInfo<DutchBlitzCardInformation>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Dutch Blitz";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;

        bool IGameInfo.CanAutoSave => false;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Portrait;

        int ICardInfo<DutchBlitzCardInformation>.CardsToPassOut => 0;

        CustomBasicList<int> ICardInfo<DutchBlitzCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<DutchBlitzCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<DutchBlitzCardInformation>.ReshuffleAllCardsFromDiscard => true;

        bool ICardInfo<DutchBlitzCardInformation>.ShowMessageWhenReshuffling => false;

        bool ICardInfo<DutchBlitzCardInformation>.PassOutAll => false;

        bool ICardInfo<DutchBlitzCardInformation>.PlayerGetsCards => false;

        bool ICardInfo<DutchBlitzCardInformation>.NoPass => true; 

        bool ICardInfo<DutchBlitzCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<DutchBlitzCardInformation> ICardInfo<DutchBlitzCardInformation>.DummyHand { get; set; } = new DeckRegularDict<DutchBlitzCardInformation>();

        bool ICardInfo<DutchBlitzCardInformation>.HasDrawAnimation => false;

        bool ICardInfo<DutchBlitzCardInformation>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<DutchBlitzCardInformation>.DiscardExcludeList(IListShuffler<DutchBlitzCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}