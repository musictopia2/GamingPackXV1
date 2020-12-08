using BasicGameFrameworkLibrary.Dice;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public interface ISavedDiceList<D>
        where D : IStandardDice, new()
    {
        DiceList<D> DiceList { get; set; }
    }
}