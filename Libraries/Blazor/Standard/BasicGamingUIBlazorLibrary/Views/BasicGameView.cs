using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
namespace BasicGamingUIBlazorLibrary.Views
{
    //i like the idea of inheriting of even key so it can use it if necessary.
    public abstract class BasicGameView<V> : KeyComponentBase, IDisposable
        where V : BlazorScreenViewModel, IBlankGameVM
    {

        //this is for subscreens.
        //worked really well so far
        [CascadingParameter]
        public V? DataContext { get; set; }
        protected override void OnInitialized()
        {
            
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            DataContext!.CommandContainer.AddAction(ShowChange); //try here for now.
            base.OnParametersSet();
        }

        void IDisposable.Dispose()
        {
            DataContext!.CommandContainer.RemoveAction(ShowChange);
        }
        protected void ShowChange() //decided to make it protected so if somehow it needs to refresh, can be done.
        {
            InvokeAsync(StateHasChanged);
        }
        

    }
}