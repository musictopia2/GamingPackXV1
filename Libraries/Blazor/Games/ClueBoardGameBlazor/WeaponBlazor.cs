using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using ClueBoardGameCP.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
namespace ClueBoardGameBlazor
{
    public class WeaponBlazor : ComponentBase
    {
        [Parameter]
        public WeaponInfo? Weapon { get; set; }
        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; }
        protected override void OnInitialized()
        {
            MainGraphics!.OriginalSize = Weapon!.DefaultSize;
            base.OnInitialized();
        }
        private string GetFileName => $"{Weapon!.Weapon}.svg";
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ISvg svg = MainGraphics!.GetMainSvg(false);
            SvgRenderClass render = new SvgRenderClass();
            Image image = new Image();
            image.PopulateFullExternalImage(this, GetFileName);
            svg.Children.Add(image);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}