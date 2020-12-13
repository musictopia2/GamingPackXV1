using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGamingUIBlazorLibrary.Extensions;
using DutchBlitzCP.Cards;
using Microsoft.AspNetCore.Components;
using System;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace DutchBlitzBlazor
{
    public partial class MultiplePilesBlazor : IDisposable
    {
        [CascadingParameter]
        public int TargetHeight { get; set; }
        [Parameter]
        public BasicMultiplePilesCP<DutchBlitzCardInformation>? Piles { get; set; }
        [Parameter]
        public string AnimationTag { get; set; } = "";
        [Parameter]
        public bool Inline { get; set; } = true;
        private string RealHeight => TargetHeight.HeightString();
        private CommandContainer? _command;
        protected override void OnInitialized()
        {
            _command = Resolve<CommandContainer>();
            _command.AddAction(ShowChange);
            base.OnInitialized();
        }
        private void ShowChange()
        {
            InvokeAsync(StateHasChanged);
        }
        void IDisposable.Dispose()
        {
            _command!.RemoveAction(ShowChange);
        }
    }
}