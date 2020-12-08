using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public interface IMultiplayerSaveState
    {
        Task DeleteGameAsync();
        Task SaveStateAsync<T>(T thisState)
            where T : IMappable, new();
        Task<string> SavedDataAsync<T>()
            where T : IMappable, new(); //blank means no data.
        Task<string> TempMultiSavedAsync();
        Task<EnumRestoreCategory> SinglePlayerRestoreCategoryAsync();
        Task<EnumRestoreCategory> MultiplayerRestoreCategoryAsync();
    }
}