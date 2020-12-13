using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using MonasteryCardGameCP.Data;
using MonasteryCardGameCP.ViewModels;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace MonasteryCardGameBlazor
{
    public partial class MissionUI
    {
        [Parameter]
        public string MissionColumnWidth { get; set; } = "";
        [Parameter]
        public CustomBasicList<MissionList>? Missions { get; set; }
        [CascadingParameter]
        public MonasteryCardGameMainViewModel? DataContext { get; set; }
        private string DescriptionMethod => nameof(MonasteryCardGameMainViewModel.MissionDetailAsync);
        private string SelectMethod => nameof(MonasteryCardGameMainViewModel.SelectPossibleMission);
        private string CompleteMethod => nameof(MonasteryCardGameMainViewModel.CompleteChosenMissionAsync);
        private string GetRowsColumn => aa.RepeatAuto(3); //to be 3 by 3.
        private string BackgroundColor(MissionList mission)
        {
            if (mission.Description == DataContext!.MissionChosen)
            {
                return cc.LimeGreen;
            }
            return cc.Aqua;
        }
    }
}