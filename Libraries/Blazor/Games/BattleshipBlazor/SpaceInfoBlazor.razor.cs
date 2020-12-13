using BattleshipCP.Data;
using BattleshipCP.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BattleshipBlazor
{
    public partial class SpaceInfoBlazor
    {
        [CascadingParameter]
        public string TargetHeight { get; set; } = ""; //hopefully this simple
        [Parameter]
        public ShipInfoCP? Ship { get; set; }
        [CascadingParameter]
        public BattleshipMainViewModel? DataContext { get; set; }
        private string PieceColor(PieceInfoCP piece)
        {
            if (piece.DidHit)
            {
                return cc.Red.ToWebColor();
            }
            return cc.Gray.ToWebColor();
        }
        private string ShipColor()
        {
            if (DataContext!.ShipSelected == Ship!.ShipCategory)
            {
                return cc.LimeGreen.ToWebColor();
            }
            return cc.Orange.ToWebColor();
        }
        private void ChooseShip()
        {
            if (DataContext!.CanChooseShip() == false || DataContext.CommandContainer.IsExecuting == true)
            {
                return;
            }
            DataContext.ChooseShip(Ship!.ShipCategory);
            DataContext.CommandContainer.UpdateAll(); //has to manually update all this time.
        }
    }
}