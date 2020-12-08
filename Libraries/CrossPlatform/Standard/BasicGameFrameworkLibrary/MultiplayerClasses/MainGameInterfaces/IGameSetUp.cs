using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using System;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces
{
    public interface IGameSetUp<P, S> : IBasicGameProcesses<P>
        where P : class, IPlayerItem, new()
        where S : BasicSavedGameClass<P>
    {
        Task SetUpGameAsync(bool isBeginning);
        Task PopulateSaveRootAsync();
        S SaveRoot { get; set; }
        Func<bool, Task>? FinishUpAsync { get; set; }
        Task StartNewTurnAsync(); //sometimes, it has to startnewturn if its beginning.
        bool ComputerEndsTurn { set; }
        Task ContinueTurnAsync();
        Task FinishGetSavedAsync();
    }
}