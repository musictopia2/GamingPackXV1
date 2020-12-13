using BackgammonCP.Graphics;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace BackgammonBlazor
{
    public partial class BackgammonClickBoardBlazor
    {
        [CascadingParameter]
        public GameBoardGraphicsCP? GraphicsData { get; set; }
        private async Task SpaceClickedAsync(int index)
        {
            await GraphicsData!.GameContainer.ProcessCustomCommandAsync(GraphicsData.GameContainer.MakeMoveAsync!, index);
        }
    }
}