using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using GermanWhistCP.Cards;
namespace GermanWhistCP.Data
{
    [SingletonGame]
    public class GermanWhistSaveInfo : BasicSavedTrickGamesClass<EnumSuitList, GermanWhistCardInformation, GermanWhistPlayerItem>
    {
        public bool WasEnd { get; set; }
    }
}