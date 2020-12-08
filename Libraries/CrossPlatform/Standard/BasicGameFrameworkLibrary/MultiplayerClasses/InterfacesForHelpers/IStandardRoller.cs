using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers
{
    /// <summary>
    /// this is everything the roll processes need to perform the work.
    /// part of it is the cup.  
    /// </summary>
    public interface IStandardRoller<D, P> : IBasicGameProcesses<P>
        where D : IStandardDice, new()
        where P : class, IPlayerItem, new()
    {
        DiceCup<D>? Cup { get; } //for sure needs to know about a cup which is used to roll the dice.
        Task AfterRollingAsync();
        Task AfterSelectUnselectDiceAsync();
        IGamePackageResolver MainContainer { get; set; }
    }
}