using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using SixtySix2PlayerCP.Cards;
namespace SixtySix2PlayerCP.Data
{
    [SingletonGame]
    public class SixtySix2PlayerDetailClass : IGameInfo, ICardInfo<SixtySix2PlayerCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.Rounds;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "Sixty Six (2 Player)";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 2;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Portrait;

        int ICardInfo<SixtySix2PlayerCardInformation>.CardsToPassOut => 6;

        CustomBasicList<int> ICardInfo<SixtySix2PlayerCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<SixtySix2PlayerCardInformation>.AddToDiscardAtBeginning => true;

        bool ICardInfo<SixtySix2PlayerCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<SixtySix2PlayerCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<SixtySix2PlayerCardInformation>.PassOutAll => false;

        bool ICardInfo<SixtySix2PlayerCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<SixtySix2PlayerCardInformation>.NoPass => false;

        bool ICardInfo<SixtySix2PlayerCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<SixtySix2PlayerCardInformation> ICardInfo<SixtySix2PlayerCardInformation>.DummyHand { get; set; } = new DeckRegularDict<SixtySix2PlayerCardInformation>();

        bool ICardInfo<SixtySix2PlayerCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<SixtySix2PlayerCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => false;

        bool ITrickData.HasTrump => true;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => false;

        CustomBasicList<int> ICardInfo<SixtySix2PlayerCardInformation>.DiscardExcludeList(IListShuffler<SixtySix2PlayerCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}