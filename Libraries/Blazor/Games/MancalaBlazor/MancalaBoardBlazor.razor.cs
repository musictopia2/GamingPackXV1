using MancalaCP.Data;
using MancalaCP.ViewModels;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace MancalaBlazor
{
    public partial class MancalaBoardBlazor
    {

        [Parameter]
        public GameBoardVM? DataContext { get; set; }
        private int StrokeWidth(SpaceInfo space)
        {
            int index = DataContext!.GetIndex(space);
            if (index == DataContext.GameData.SpaceSelected || index == DataContext.GameData.SpaceStarted)
            {
                return 4;
            }
            return 2;
        }
        private string StrokeColor(SpaceInfo space)
        {
            int index = DataContext!.GetIndex(space);
            if (index == DataContext.GameData.SpaceSelected)
            {
                return cc.Red;
            }
            if (index == DataContext.GameData.SpaceStarted)
            {
                return cc.Green;
            }
            return cc.Black;
        }
    }
}