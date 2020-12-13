using Microsoft.AspNetCore.Components;
using SkipboCP.Data;
using SkipboCP.ViewModels;
using System;
namespace SkipboBlazor.Views
{
    public partial class PlayerPilesView : IDisposable
    {
        [CascadingParameter]
        public PlayerPilesViewModel? DataContext { get; set; }
        private SkipboPlayerItem? _player;
        protected override void OnParametersSet()
        {
            _player = DataContext!.GameContainer.PlayerList!.GetWhoPlayer();

            base.OnParametersSet();
        }
        private string DiscardTag => $"discard{_player!.NickName}";
        private string StockTag => $"stock{_player!.NickName}";
        protected override void OnInitialized()
        {
            DataContext!.GameContainer.Command.AddAction(ShowChange);
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
            DataContext!.GameContainer.Command.RemoveAction(ShowChange);
        }
    }
}