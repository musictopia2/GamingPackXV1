using BasicGameFrameworkLibrary.MahjongTileClasses;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
namespace MahjongTilesBlazor
{
    public class MahjongTilesComponent : BaseDeckGraphics<MahjongSolitaireTileInfo>
    {
        protected override bool NeedsCommand()
        {
            return DeckObject!.IsEnabled; //try this way.
            //return base.NeedsCommand();
        }

        public MahjongTilesComponent() { }
        protected override SizeF DefaultSize { get; } = new SizeF(136, 176);
        protected override bool NeedsToDrawBacks { get; } = false;
        protected override bool ShowDisabledColors => true;
        protected override bool CanStartDrawing()
        {
            //if (DeckObject!.IsEnabled == false)
            //{
            //    return false; //try this for now.
            //}
            return true;
        }

        protected override void DrawBacks()
        {
            //has no backs.
        }

        protected override void DrawImage() //what makes this one so big is the mahjong tiles.
        {
            //this is where i focus on the image i am drawing.
            Image image = new Image();
            image.PopulateFullExternalImage(this, $"{DeckObject!.Index}.svg");
            PopulateImage(image);
            MainGroup!.Children.Add(image); //hopefully this simple.
        }
    }
}