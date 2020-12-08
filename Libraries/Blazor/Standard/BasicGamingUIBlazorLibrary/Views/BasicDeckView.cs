using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.Views
{
    public class BasicDeckView<V> : BasicGameView<V>
        where V : BlazorScreenViewModel, IBlankGameVM
    {
        [CascadingParameter]
        private int TargetHeight { get; set; }
        protected string HeightString => $"{TargetHeight}vh"; //if everything works out well then don't need the event aggrevation anymore.
    }
}