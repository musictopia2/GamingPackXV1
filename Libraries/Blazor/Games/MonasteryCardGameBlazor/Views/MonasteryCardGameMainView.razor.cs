using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using MonasteryCardGameCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using MonasteryCardGameCP.Data;
namespace MonasteryCardGameBlazor.Views
{
    public partial class MonasteryCardGameMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private MonasteryCardGameVMData? _vmData;
        private MonasteryCardGameGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<MonasteryCardGameVMData>();
            _gameContainer = cons.Resolve<MonasteryCardGameGameContainer>();
            _labels.AddLabel("Turn", nameof(MonasteryCardGameMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(MonasteryCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards", false, nameof(MonasteryCardGamePlayerItem.ObjectCount))
                .AddColumn("Done", false, nameof(MonasteryCardGamePlayerItem.FinishedCurrentMission), category: EnumScoreSpecialCategory.TrueFalse);
            int x;
            for (x = 1; x <= 9; x++)
            {
                _scores.AddColumn($"M{x}", false, $"Mission{x}Completed", category: EnumScoreSpecialCategory.TrueFalse);
            }
            base.OnInitialized();
        }

    }
}