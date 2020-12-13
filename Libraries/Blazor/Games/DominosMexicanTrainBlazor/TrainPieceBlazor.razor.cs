using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using DominosMexicanTrainCP.Data;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using static DominosMexicanTrainCP.Logic.TrainStationBoardProcesses;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace DominosMexicanTrainBlazor
{
    public partial class TrainPieceBlazor
    {
        [Parameter]
        public TrainInfo? TrainInfo { get; set; }
        [Parameter]
        public bool WasDouble { get; set; }
        [Parameter]
        public bool Satisfy { get; set; }
        [Parameter]
        public PrivateTrain PositionInfo { get; set; } 
        [Parameter]
        public int Player { get; set; }
        [Parameter]
        public bool Self { get; set; }
        [Parameter]
        public EventCallback<int> TrainClicked { get; set; }
        private SizeF TrainSize { get; set; } = new SizeF(75, 46);
        private string GetFill()
        {
            if (TrainInfo!.IsPublic)
            {
                return cc.Green.ToWebColor();
            }
            return cc.Blue.ToWebColor();
        }
        private bool CanClick()
        {
            if (WasDouble == true && Satisfy == false)
            {
                return false;
            }
            return true;
        }
        private bool CanShowTrains()
        {
            if (WasDouble == true && Satisfy == false)
            {
                return false;
            }
            return TrainInfo!.TrainUp || Satisfy;
        }
    }
}