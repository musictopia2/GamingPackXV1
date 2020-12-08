using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface ISaveSinglePlayerClass
    {
        Task<bool> CanOpenSavedSinglePlayerGameAsync(); //could be async even for this part (we never know).
        Task DeleteSinglePlayerGameAsync(); //deleting it could even be just setting occurance to null so i can go back to it if necessary.
        Task<T> RetrieveSinglePlayerGameAsync<T>()
            where T : IMappable, new();
        Task SaveSimpleSinglePlayerGameAsync<T>(T thisObject)
            where T : IMappable, new();
    }
}