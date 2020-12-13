using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace YachtRaceCP.Data
{
    [SingletonGame]
    public class YachtRaceSaveInfo : BasicSavedDiceClass<SimpleDice, YachtRacePlayerItem> { }
}