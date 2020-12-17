using BasicGameFrameworkLibrary.CommandClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    public partial class BlankClickableSquare
    {
        [Parameter]
        public EventCallback SpaceClicked { get; set; } //hopefully this works (?)
        [Parameter]
        public SizeF SpaceSize { get; set; }
        [Parameter]
        public PointF SpaceLocation { get; set; }
        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public bool UseLargerSquares { get; set; }




        private readonly CommandContainer _command;

        private float GetX()
        {
            if (UseLargerSquares == false)
            {
                return SpaceLocation.X;
            }
            var diffs = SpaceSize.Width / 2;
            return SpaceLocation.X - diffs;
        }

        private float GetY()
        {
            if (UseLargerSquares == false)
            {
                return SpaceLocation.Y;
            }
            var diffs = SpaceSize.Width / 2;
            return SpaceLocation.Y - diffs;
        }

        private float GetWidth()
        {
            if (UseLargerSquares == false)
            {
                return SpaceSize.Width;
            }
            return SpaceSize.Width * 2;
        }

        private float GetHeight()
        {
            if (UseLargerSquares == false)
            {
                return SpaceSize.Height;
            }
            return SpaceSize.Height * 2;
        }


        public BlankClickableSquare()
        {
            _command = cons!.Resolve<CommandContainer>();
        }

        protected override bool ShouldRender()
        {
            return !Fixed;
        }

        private async Task PrivateClickAsync()
        {
            _command.StartExecuting(); //try here so all board games would have it accounted for.
            await SpaceClicked.InvokeAsync();
        }
    }
}