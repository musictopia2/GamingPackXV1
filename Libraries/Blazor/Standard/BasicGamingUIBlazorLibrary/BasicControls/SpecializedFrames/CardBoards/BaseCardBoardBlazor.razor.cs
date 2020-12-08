using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGamingUIBlazorLibrary.Extensions;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.CardBoards
{
    public partial class BaseCardBoardBlazor<D>
        where D : class, IDeckObject, new()
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } //i like this idea.  this seems to be working really well.
        [Parameter]
        public GameBoardObservable<D>? DataContext { get; set; } //only iffy part is new game.
        [Parameter]
        public RenderFragment<D>? ChildContent { get; set; }
        private SizeF DefaultSize { get; set; }
        protected override void OnInitialized()
        {
            D obj = new D();
            DefaultSize = obj.DefaultSize;
            base.OnInitialized();
        }
        private SizeF GetViewSize()
        {
            return new SizeF(DefaultSize.Width * DataContext!.Columns, DefaultSize.Height * DataContext.Rows);
        }
        private string GetTargetString => (TargetHeight * DataContext!.Rows).HeightString();
        
    }
}