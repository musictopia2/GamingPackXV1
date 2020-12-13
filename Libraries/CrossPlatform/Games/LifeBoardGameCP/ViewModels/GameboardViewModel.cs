using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommonInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
namespace LifeBoardGameCP.ViewModels
{
    [InstanceGame]
    public class GameboardViewModel : BlazorScreenViewModel, IMainScreen { }  
}