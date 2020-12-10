using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using BlackjackCP.ViewModels;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Components.Basic;
using System.Reflection.Emit;
using BasicBlazorLibrary.Helpers;
namespace BlackjackBlazor.Views
{
    public partial class BlackjackMainView
    {
        private CustomBasicList<LabelGridModel> WinLabels { get; set; } = new CustomBasicList<LabelGridModel>();
        private CustomBasicList<LabelGridModel> PointLabels { get; set; } = new CustomBasicList<LabelGridModel>();

        protected override void OnInitialized()
        {

            WinLabels.Clear();
            WinLabels.AddLabel("Wins", nameof(BlackjackMainViewModel.Wins))
                .AddLabel("Losses", nameof(BlackjackMainViewModel.Losses))
                .AddLabel("Draws", nameof(BlackjackMainViewModel.Draws));
            PointLabels.Clear();
            PointLabels.AddLabel("Human Points", nameof(BlackjackMainViewModel.HumanPoints))
                .AddLabel("Computer Points", nameof(BlackjackMainViewModel.ComputerPoints));
            //if you have to add command change, do so as well.
            base.OnInitialized();
        }
        private string StayName => nameof(BlackjackMainViewModel.StayAsync);
        private string HitName => nameof(BlackjackMainViewModel.HitAsync);
        private string AceName => nameof(BlackjackMainViewModel.AceAsync);
        private string AceChoice => nameof(BlackjackMainViewModel.NeedsAceChoice);
    }
}