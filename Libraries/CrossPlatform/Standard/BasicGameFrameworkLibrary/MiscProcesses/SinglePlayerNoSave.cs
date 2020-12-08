using BasicGameFrameworkLibrary.CommonInterfaces;
using CommonBasicStandardLibraries.Exceptions;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MiscProcesses
{
    public class SinglePlayerNoSave : ISaveSinglePlayerClass
    {
        Task<bool> ISaveSinglePlayerClass.CanOpenSavedSinglePlayerGameAsync()
        {
            return Task.FromResult(false);
        }
        Task ISaveSinglePlayerClass.DeleteSinglePlayerGameAsync()
        {
            return Task.CompletedTask;
        }
        Task<T> ISaveSinglePlayerClass.RetrieveSinglePlayerGameAsync<T>()
        {
            throw new BasicBlankException("You should not have retrieved the game because there is no game to retrieve.");
        }
        Task ISaveSinglePlayerClass.SaveSimpleSinglePlayerGameAsync<T>(T thisObject)
        {
            return Task.CompletedTask; //this acts like nothing is happening.
        }
    }
}