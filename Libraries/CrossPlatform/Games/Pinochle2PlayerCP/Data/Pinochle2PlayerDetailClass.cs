using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Pinochle2PlayerCP.Cards;
namespace Pinochle2PlayerCP.Data
{
    [SingletonGame]
    public class Pinochle2PlayerDetailClass : IGameInfo, ICardInfo<Pinochle2PlayerCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Pinochle (2 Player)";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 2;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<Pinochle2PlayerCardInformation>.CardsToPassOut => 12;

        CustomBasicList<int> ICardInfo<Pinochle2PlayerCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<Pinochle2PlayerCardInformation>.AddToDiscardAtBeginning => true;

        bool ICardInfo<Pinochle2PlayerCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<Pinochle2PlayerCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<Pinochle2PlayerCardInformation>.PassOutAll => false;

        bool ICardInfo<Pinochle2PlayerCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<Pinochle2PlayerCardInformation>.NoPass => false;

        bool ICardInfo<Pinochle2PlayerCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<Pinochle2PlayerCardInformation> ICardInfo<Pinochle2PlayerCardInformation>.DummyHand { get; set; } = new DeckRegularDict<Pinochle2PlayerCardInformation>();

        bool ICardInfo<Pinochle2PlayerCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<Pinochle2PlayerCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => false;

        bool ITrickData.HasTrump => false;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => false;

        CustomBasicList<int> ICardInfo<Pinochle2PlayerCardInformation>.DiscardExcludeList(IListShuffler<Pinochle2PlayerCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}