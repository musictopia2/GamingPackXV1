using CommonBasicStandardLibraries.CollectionClasses;
using CountdownCP.Data;
using Microsoft.AspNetCore.Components;
namespace CountdownBlazor
{
    public partial class GameBoardBlazor
    {
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public CountdownGameContainer? GameContainer { get; set; }

        private CustomBasicList<CountdownPlayerItem> _players = new CustomBasicList<CountdownPlayerItem>();
        protected override void OnInitialized()
        {
            _players = GameContainer!.GetPlayerList();
            base.OnInitialized();
        }
    }
}