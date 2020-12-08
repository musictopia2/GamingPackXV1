using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.YahtzeeControls
{
    public partial class YahtzeeField
    {
        [CascadingParameter]
        public int BottomDescriptionWidth { get; set; } = 500; //kismet will require extras.  hopefully will be easy enough to adjust for kismet or other yahtzee games (?)
        [Parameter]
        public int Column { get; set; }
        [Parameter]
        public int Row { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Highlighted { get; set; }
        [Parameter]
        public bool SpanColumn { get; set; } //if true, then do something different.
        [CascadingParameter]
        public bool IsBottom { get; set; } = false;
        [Parameter]
        public bool Extra { get; set; }
        private int GetX
        {
            get
            {
                if (IsBottom == false)
                {
                    return (Column - 1) * 300;
                }
                if (Column == 1)
                {
                    return 0;
                }
                if (Column == 2)
                {
                    return BottomDescriptionWidth;
                }
                if (Column == 3)
                {
                    return BottomDescriptionWidth + 300;
                }
                return 10; //make it obvious there is a bug.
            }
        }
        private int GetY => (Row - 1) * 200;
        private string GetColor()
        {
            if (Extra)
            {
                return "Aqua";
            }

            if (Highlighted)
            {
                return "Yellow";
            }
            return "White";
        }
        private int GetWidth()
        {
            if (IsBottom == false)
            {
                if (SpanColumn == false)
                {
                    return 300;
                }
                return 600;
            }
            if (SpanColumn)
            {
                return BottomDescriptionWidth + 300;
            }
            if (Column == 1)
            {
                return BottomDescriptionWidth;
            }
            return 300;
        }
    }
}