using BasicGameFrameworkLibrary.Dice;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers
{
    public interface ICup<D>
        where D : IStandardDice, new()
    {
        DiceCup<D>? Cup { get; set; } //for sure needs to know about a cup which is used to roll the dice.
    }
}