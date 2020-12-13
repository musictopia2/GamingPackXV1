using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CribbageCP.Logic;
namespace CribbageCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class CribbageDetailClass : IGameInfo, ICardInfo<CribbageCard>
    {
        private readonly CribbageDelegates _delegates;

        public CribbageDetailClass(CribbageDelegates delegates)
        {
            _delegates = delegates;
        }
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Cribbage";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 3;

        bool IGameInfo.CanAutoSave => false;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<CribbageCard>.CardsToPassOut
        {
            get
            {
                if (_delegates.GetPlayerCount == null)
                {
                    throw new BasicBlankException("Nobody is handling get player count.  Rethink");
                }
                int count = _delegates.GetPlayerCount.Invoke();
                if (count == 2)
                {
                    return 6;
                }
                else if (count == 3)
                {
                    return 5;
                }
                else
                {
                    throw new BasicBlankException("Only 2 or 3 players are supported");
                }
            }
        }
        CustomBasicList<int> ICardInfo<CribbageCard>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<CribbageCard>.AddToDiscardAtBeginning => true;

        bool ICardInfo<CribbageCard>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<CribbageCard>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<CribbageCard>.PassOutAll => false;

        bool ICardInfo<CribbageCard>.PlayerGetsCards => true;

        bool ICardInfo<CribbageCard>.NoPass => false;

        bool ICardInfo<CribbageCard>.NeedsDummyHand => false;

        DeckRegularDict<CribbageCard> ICardInfo<CribbageCard>.DummyHand { get; set; } = new DeckRegularDict<CribbageCard>();

        bool ICardInfo<CribbageCard>.HasDrawAnimation => true;

        bool ICardInfo<CribbageCard>.CanSortCardsToBeginWith => true;

        CustomBasicList<int> ICardInfo<CribbageCard>.DiscardExcludeList(IListShuffler<CribbageCard> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}