using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using GolfCardGameCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using GolfCardGameCP.Data;
namespace GolfCardGameBlazor.Views
{
    public partial class GolfCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        [CascadingParameter]
        public GolfCardGameVMData? VMData { get; set; }
        private GolfCardGameGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _gameContainer = cons!.Resolve<GolfCardGameGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(GolfCardGameMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(GolfCardGameMainViewModel.Status))
               .AddLabel("Round", nameof(GolfCardGameMainViewModel.Round))
               .AddLabel("Instructions", nameof(GolfCardGameMainViewModel.Instructions));
            _scores.Clear();
            _scores.AddColumn("Knocked", false, nameof(GolfCardGamePlayerItem.Knocked), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Changed 1", false, nameof(GolfCardGamePlayerItem.FirstChanged), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Changed 2", false, nameof(GolfCardGamePlayerItem.SecondChanged), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Previous Score", false, nameof(GolfCardGamePlayerItem.PreviousScore))
                .AddColumn("Total Score", false, nameof(GolfCardGamePlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string KnockMethod => nameof(GolfCardGameMainViewModel.KnockAsync);
    }
}