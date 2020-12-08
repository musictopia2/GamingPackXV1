using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ViewModelInterfaces
{
    public interface INewRoundVM : IBlazorScreen
    {
        bool CanStartNewRound { get; }
        Task StartNewRoundAsync();
    }
}