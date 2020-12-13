using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using RummyDiceCP.Data;
using System;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace RummyDiceBlazor
{
    public partial class RummyGraphicsBlazor
    {

        

        [Parameter]
        public RummyDiceInfo? DiceInfo { get; set; }

        [Parameter]
        public string TargetHeight { get; set; } = ""; //i think it should be this way.


        //for this one, looks like there was eventcallback.
        //if i do this for the graphicscommand, then what would end up happening is the following:

        //the graphicscommand would be generic but anything.  that is what would be sent back.
        //its possible that 2 versions of graphicscommand may be needed if one does not require return value.

        //i know that any cards, etc would need it to attempt to fix additional performance issues.
        

        [Parameter]
        public EventCallback<RummyDiceInfo> OnClick { get; set; }
        private string GetFillColor()
        {
            return DiceInfo!.Color.ToColor().ToWebColor();
        }
        private string WhiteString()
        {
            return $"<text x='50%' y='55%' font-family='tahoma' font-size='70' stroke-width='2px' stroke='black' fill='white' dominant-baseline='middle' text-anchor='middle' >{DiceInfo!.Display}</text>";
        }
        private string GetBorderColor()
        {
            return cc.White.ToWebColor();
        }

        //looks like for rummy dice, it is somehow smart enough to only dispose what it needs.
        //adding to the list does not dispose previous ones.

        //this means i need to look further for a pattern.

    }
}