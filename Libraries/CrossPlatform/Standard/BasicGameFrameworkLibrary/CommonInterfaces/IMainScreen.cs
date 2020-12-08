using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    /// <summary>
    /// the purpose of this is to make sure i don't use the wrong name since everything the shell uses must implement this even though it does nothing.
    /// this also deals with screens  for now, has to be blazor screens.
    /// </summary>
    public interface IMainScreen : IBlazorScreen { }
}