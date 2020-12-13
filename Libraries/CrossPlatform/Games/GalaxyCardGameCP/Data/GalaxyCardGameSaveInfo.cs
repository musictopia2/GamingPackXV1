using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using GalaxyCardGameCP.Cards;
namespace GalaxyCardGameCP.Data
{
    [SingletonGame]
    public class GalaxyCardGameSaveInfo : BasicSavedTrickGamesClass<EnumSuitList, GalaxyCardGameCardInformation, GalaxyCardGamePlayerItem>
    {
        public EnumGameStatus GameStatus { get; set; }
        public GalaxyCardGameCardInformation WinningCard { get; set; } = new GalaxyCardGameCardInformation();
        public int LastWin { get; set; }
    }
}