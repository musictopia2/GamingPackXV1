using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using FluxxCP.Cards;
namespace FluxxCP.Data
{
    [SingletonGame]
    public class FluxxDetailClass : IGameInfo, ICardInfo<FluxxCardInformation>, INewCard<FluxxCardInformation>
    {
        EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Fluxx";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 6;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<FluxxCardInformation>.CardsToPassOut => 3;

        CustomBasicList<int> ICardInfo<FluxxCardInformation>.PlayerExcludeList => new CustomBasicList<int>() { 1 };

        bool ICardInfo<FluxxCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<FluxxCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<FluxxCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<FluxxCardInformation>.PassOutAll => false;

        bool ICardInfo<FluxxCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<FluxxCardInformation>.NoPass => false;

        bool ICardInfo<FluxxCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<FluxxCardInformation> ICardInfo<FluxxCardInformation>.DummyHand { get; set; } = new DeckRegularDict<FluxxCardInformation>();

        bool ICardInfo<FluxxCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<FluxxCardInformation>.CanSortCardsToBeginWith => true;

        FluxxCardInformation INewCard<FluxxCardInformation>.GetNewCard(int chosen)
        {
            return GetNewCard(chosen);
        }
        public static FluxxCardInformation GetNewCard(int chosen)
        {
            if (chosen <= 0)
            {
                throw new BasicBlankException("Must choose a value above 0 for GetNewCard for Fluxx");
            }
            if (chosen <= 22)
            {
                return new RuleCard();
            }
            if (chosen <= 40)
            {
                return new KeeperCard();
            }
            if (chosen <= 63)
            {
                return new GoalCard();
            }
            if (chosen <= 83)
            {
                return new ActionCard();
            }
            throw new BasicBlankException("Must go only up to 83");
        }
        CustomBasicList<int> ICardInfo<FluxxCardInformation>.DiscardExcludeList(IListShuffler<FluxxCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}