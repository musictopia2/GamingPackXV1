using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace A21DiceGameCP.Data
{
    [SingletonGame]
    public class A21DiceGameSaveInfo : BasicSavedDiceClass<SimpleDice, A21DiceGamePlayerItem>
    {
        public bool IsFaceOff { get; set; }
    }
}