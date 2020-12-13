using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using RackoCP.Cards;
namespace RackoCP.Data
{
    [SingletonGame]
    public class RackoSaveInfo : BasicSavedCardClass<RackoPlayerItem, RackoCardInformation> { }
}