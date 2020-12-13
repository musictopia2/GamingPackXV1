using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using GermanWhistCP.Cards;
namespace GermanWhistCP.Data
{
    [SingletonGame]
    public class GermanWhistDetailClass : IGameInfo, ICardInfo<GermanWhistCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

        string IGameInfo.GameName => "German Whist";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 2;

        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice;

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

        int ICardInfo<GermanWhistCardInformation>.CardsToPassOut => 13;

        CustomBasicList<int> ICardInfo<GermanWhistCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<GermanWhistCardInformation>.AddToDiscardAtBeginning => false;

        bool ICardInfo<GermanWhistCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<GermanWhistCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<GermanWhistCardInformation>.PassOutAll => false;

        bool ICardInfo<GermanWhistCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<GermanWhistCardInformation>.NoPass => false;

        bool ICardInfo<GermanWhistCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<GermanWhistCardInformation> ICardInfo<GermanWhistCardInformation>.DummyHand { get; set; } = new DeckRegularDict<GermanWhistCardInformation>();

        bool ICardInfo<GermanWhistCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<GermanWhistCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => true;

        bool ITrickData.HasTrump => true;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => false;

        CustomBasicList<int> ICardInfo<GermanWhistCardInformation>.DiscardExcludeList(IListShuffler<GermanWhistCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }
    }
}