using FlinchCP.Data;
namespace FlinchBlazor.Views
{
    public partial class PlayerPilesView
    {
        private FlinchPlayerItem? _player;
        protected override void OnParametersSet()
        {
            _player = DataContext!.GameContainer.PlayerList!.GetWhoPlayer();

            base.OnParametersSet();
        }
        private string DiscardTag => $"discard{_player!.NickName}";
        private string StockTag => $"stock{_player!.NickName}";
    }
}