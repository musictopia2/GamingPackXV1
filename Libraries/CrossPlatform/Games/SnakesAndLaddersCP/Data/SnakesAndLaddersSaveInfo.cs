using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace SnakesAndLaddersCP.Data
{
    [SingletonGame]
    public class SnakesAndLaddersSaveInfo : BasicSavedGameClass<SnakesAndLaddersPlayerItem>
    {
        public DiceList<SimpleDice> DiceList = new DiceList<SimpleDice>();
        public bool HasRolled { get; set; }
    }
}