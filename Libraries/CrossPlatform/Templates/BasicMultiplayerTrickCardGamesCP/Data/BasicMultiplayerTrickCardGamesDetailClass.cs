using System;
using System.Text;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Linq;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using fs = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.FileHelpers;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicMultiplayerTrickCardGamesCP.Cards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
//i think this is the most common things i like to do
namespace BasicMultiplayerTrickCardGamesCP.Data
{
    [SingletonGame]
    public class BasicMultiplayerTrickCardGamesDetailClass : IGameInfo, ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>, ITrickData
    {
        EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

        bool IGameInfo.CanHaveExtraComputerPlayers => false;

        EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

        EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;

        string IGameInfo.GameName => "Game";

        int IGameInfo.NoPlayers => 0;

        int IGameInfo.MinPlayers => 2;

        int IGameInfo.MaxPlayers => 4;


        bool IGameInfo.CanAutoSave => true;

        EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice; //default to smallest but can change as needed.

        EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape; //default to portrait but can change to what is needed.

        int ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.CardsToPassOut => 7; //change to what you need.

        CustomBasicList<int> ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.PlayerExcludeList => new CustomBasicList<int>();

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.AddToDiscardAtBeginning => true;

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.ReshuffleAllCardsFromDiscard => false;

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.ShowMessageWhenReshuffling => true;

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.PassOutAll => false;

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.PlayerGetsCards => true;

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.NoPass => false;

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.NeedsDummyHand => false;

        DeckRegularDict<BasicMultiplayerTrickCardGamesCardInformation> ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.DummyHand { get; set; } = new DeckRegularDict<BasicMultiplayerTrickCardGamesCardInformation>();

        CustomBasicList<int> ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.DiscardExcludeList(IListShuffler<BasicMultiplayerTrickCardGamesCardInformation> deckList)
        {
            return new CustomBasicList<int>();
        }

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.HasDrawAnimation => true;

        bool ICardInfo<BasicMultiplayerTrickCardGamesCardInformation>.CanSortCardsToBeginWith => true;

        bool ITrickData.FirstPlayerAnySuit => true;

        bool ITrickData.FollowSuit => true;

        bool ITrickData.MustFollow => false;

        bool ITrickData.HasTrump => false;

        bool ITrickData.MustPlayTrump => false;

        EnumTrickStyle ITrickData.TrickStyle => EnumTrickStyle.None;

        bool ITrickData.HasDummy => false;


    }
}