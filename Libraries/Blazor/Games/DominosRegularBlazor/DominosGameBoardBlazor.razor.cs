using BasicGameFrameworkLibrary.Dominos;
using BasicGamingUIBlazorLibrary.Extensions;
using DominosRegularCP.Logic;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace DominosRegularBlazor
{
    public partial class DominosGameBoardBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } //i like the idea of cascading values here.
        [Parameter]
        public GameBoardCP? DataContext { get; set; }
        private string ViewBox()
        {
            SimpleDominoInfo domino = new SimpleDominoInfo();
            SizeF size = new SizeF(domino.DefaultSize.Width * 2, domino.DefaultSize.Height);
            return $"0 0 {size.Width} {size.Height}";
        }
        private string GetTargetString => TargetHeight.HeightString(); //hopefully this simple (?)
    }
}