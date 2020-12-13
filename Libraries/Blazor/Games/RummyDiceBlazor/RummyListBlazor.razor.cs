using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using RummyDiceCP.Data;
using RummyDiceCP.Logic;
using RummyDiceCP.ViewModels;
using System;
using System.Threading.Tasks;
namespace RummyDiceBlazor
{
    public partial class RummyListBlazor : IDisposable
    {
        [Parameter]
        public RummyBoardCP? GameBoard { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = ""; //this is the height of the dice being used.
        [CascadingParameter]
        public RummyDiceMainViewModel? DataContext { get; set; }
        private CustomBasicList<RummyDiceInfo> DiceList { get; set; } = new CustomBasicList<RummyDiceInfo>();

        protected override void OnInitialized()
        {
            DataContext!.CommandContainer.AddAction(ShowChange, "rummydice");
            base.OnInitialized();
        }

        private void ShowChange()
        {
            InvokeAsync(StateHasChanged);
        }


#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            DataContext!.CommandContainer.RemoveAction("rummydice");
        }

        protected override void OnParametersSet()
        {
            DiceList = DataContext!.MainGame.SaveRoot.DiceList;

            base.OnParametersSet();
        }
        private async Task OnClickAsync(RummyDiceInfo dice)
        {
            await GameBoard!.SelectDiceAsync(dice);
        }
    }
}