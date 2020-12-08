using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
namespace BasicGameFrameworkLibrary.ViewModelInterfaces
{
    public interface IBeginningColorViewModel : IBlazorScreen
    {
        string Turn { get; set; }
        string Instructions { get; set; }
    }
}