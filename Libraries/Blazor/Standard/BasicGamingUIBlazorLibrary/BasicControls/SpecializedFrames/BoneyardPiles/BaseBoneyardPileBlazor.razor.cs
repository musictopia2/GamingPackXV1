using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.BoneyardPiles
{
    public partial class BaseBoneyardPileBlazor<D, LI> : IDisposable
         where D : class, ILocationDeck, new()
        where LI : class, IScatterList<D>, new()
    {
        //had to do a workaround because was unable to scatter for now.
        [Parameter]
        public ScatteringPiecesObservable<D, LI>? BoneyardPile { get; set; }
        public void Dispose()
        {
            CommandContainer command = cons!.Resolve<CommandContainer>();
            command.RemoveAction(ShowChange);
        }
        private void ShowChange()
        {
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        protected override void OnInitialized()
        {
            CommandContainer command = cons!.Resolve<CommandContainer>();
            command.AddAction(ShowChange);
            base.OnInitialized();
        }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private async Task BoardClickedAsync()
        {
            if (BoneyardPile!.BoardCommand.CanExecute(null) == false)
            {
                return;
            }
            await BoneyardPile.BoardCommand.ExecuteAsync(null);
        }
    }
}