using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
namespace BasicGamingUIBlazorLibrary.Views
{
    public abstract partial class BasicCustomButtonView<V>
        where V : BlazorScreenViewModel, IBlankGameVM
    {
        protected abstract string MethodName { get; }
        protected abstract string DisplayName { get; }
    }
}