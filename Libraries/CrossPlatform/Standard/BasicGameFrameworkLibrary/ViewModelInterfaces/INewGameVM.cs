using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ViewModelInterfaces
{
    public interface INewGameVM : IBlazorScreen
    {
        bool CanStartNewGame();
        Task StartNewGameAsync();
    }
}