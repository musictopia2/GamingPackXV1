using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public class BasicSavedBoardDiceGameClass<P> : BasicSavedGameClass<P>, ISavedDiceList<SimpleDice>
        where P : class, IPlayerItem, new()
    {
        public DiceList<SimpleDice> DiceList { get; set; } = new DiceList<SimpleDice>();
    }
}