using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using SkipboCP.Cards;
using SkipboCP.Logic;
namespace SkipboCP.Data
{
    [SingletonGame]
    public class SkipboDetailClass : IGameInfo, ICardInfo<SkipboCardInformation>
    {
        private readonly SkipboDelegates _delegates;
        public SkipboDetailClass(SkipboDelegates delegates)
        {
            _delegates = delegates;
        }
        EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

        bool IGameInfo.CanHaveExtraComputerPlayers => true;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "SkipBo";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Portrait;

        int ICardInfo<SkipboCardInformation>.CardsToPassOut
        {
            get
            {
                if (_delegates.GetPlayerCount == null)
                {
                    throw new BasicBlankException("Nobody is getting player count.  Rethink");
                }
                int count = _delegates.GetPlayerCount.Invoke();
                if (count == 2)
                {
                    return 30;
                }
                return 20;
            }
        }

        CustomBasicList<int> ICardInfo<SkipboCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<SkipboCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<SkipboCardInformation>.ReshuffleAllCardsFromDiscard => true;

        bool ICardInfo<SkipboCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<SkipboCardInformation>.PassOutAll => false;

        bool ICardInfo<SkipboCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<SkipboCardInformation>.NoPass => false;

        bool ICardInfo<SkipboCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<SkipboCardInformation> ICardInfo<SkipboCardInformation>.DummyHand { get; set; } = new DeckRegularDict<SkipboCardInformation>();

        bool ICardInfo<SkipboCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<SkipboCardInformation>.CanSortCardsToBeginWith => false;

        CustomBasicList<int> ICardInfo<SkipboCardInformation>.DiscardExcludeList(IListShuffler<SkipboCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}