using BasicBlazorLibrary.Components.Basic;
using Microsoft.AspNetCore.Components;
using System;
namespace FluxxBlazor
{
    public class SimpleActionView : KeyComponentBase, IDisposable
    {
        [CascadingParameter]
        public CompleteContainerClass? CompleteContainer { get; set; }
        protected override void OnInitialized()
        {
            CompleteContainer!.GameContainer.Command.AddAction(ShowChange);
            base.OnInitialized();
        }
        private void ShowChange()
        {
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        void IDisposable.Dispose()
        {
            CompleteContainer!.GameContainer.Command.RemoveAction(ShowChange);
        }
    }
}