using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using ConcentrationCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using ConcentrationCP.Data;
namespace ConcentrationBlazor.Views
{
    public partial class ConcentrationMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private ConcentrationVMData? _vmData;
        private ConcentrationGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<ConcentrationVMData>();
            _gameContainer = cons.Resolve<ConcentrationGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ConcentrationMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(ConcentrationMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Pairs", true, nameof(ConcentrationPlayerItem.Pairs));
            base.OnInitialized();
        }

    }
}