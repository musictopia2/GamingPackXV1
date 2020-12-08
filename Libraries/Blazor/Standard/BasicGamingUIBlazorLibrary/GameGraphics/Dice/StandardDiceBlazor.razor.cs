using BasicGameFrameworkLibrary.Dice;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Dice
{
    //good news is figured out the issue.  i always have to remember its false, not true.

    internal record DiceRecord(int Value, string FillColor, string DotColor, bool Selected, bool Visible,float X, float Y );
    public partial class StandardDiceBlazor : GraphicsCommand
    {
        private DiceRecord? _previous;

        protected override void OnAfterRender(bool firstRender)
        {
            if (Dice == null)
            {
                return; //to attempt to fix null reference exception issue.
            }
            _previous = GetRecord;
            base.OnAfterRender(firstRender);
        }
        protected override bool ShouldRender()
        {
            if (_previous == null || Dice == null)
            {
                return true;
            }
            return _previous!.Equals(GetRecord) == false;
        }

        private bool ReallySelected => Dice!.Hold || Dice.IsSelected;

        private DiceRecord GetRecord => new DiceRecord(Dice!.Value, Dice.FillColor, Dice.DotColor, ReallySelected, Dice.Visible, X, Y);
        [Parameter]
        public float X { get; set; }
        [Parameter]
        public float Y { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public IStandardDice? Dice { get; set; } //this means if you need to click, then needs to do twice.  otherwise graphicscommand requires generics.
        private float DiceRadius => 10;
        private string WhiteString()
        {
            return $"<text x='50%' y='55%' font-family='tahoma' font-size='80' stroke='black' fill='white' dominant-baseline='middle' text-anchor='middle' >{Dice!.Value}</text>";
        }
        private string RectString()
        {
            return $"<rect fill={GetRealColor(Dice!.FillColor)} rx=6 ry=6 x=3 y=3 width=94 height=94 stroke='black' stroke-width= '6px' />";
        }
        private string GetRealColor(string colorUsed)
        {
            if (colorUsed.Length == 0)
            {
                throw new BasicBlankException("Had no color.  Rethink");
            }
            if (colorUsed.Length != 9)
            {
                throw new BasicBlankException("Color In Wrong Format");
            }
            if (colorUsed.StartsWith("#FF") == false)
            {
                throw new BasicBlankException("Colors must start with FF so no transparency");
            }
            string output = $"#{colorUsed.Substring(3, 6)}";
            return output;
        }
    }
}