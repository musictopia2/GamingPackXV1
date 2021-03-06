using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using PaydayCP.Data;
using PaydayCP.Graphics;
using PaydayCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using System;
using System.Threading.Tasks;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace PaydayBlazor.Views
{
    public partial class PaydayMainView
    {
        private string GetColumns => $"{aa.RepeatMinimum(1)} {aa.RepeatAuto(1)}";
        private string RowData => "20vh 60vh 15vh";
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private GameBoardGraphicsCP? _graphicsData;
        private PaydayVMData? _vmData;
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<PaydayVMData>();
            _graphicsData = cons.Resolve<GameBoardGraphicsCP>();
            _labels.AddLabel("Main Turn", nameof(PaydayMainViewModel.NormalTurn))
                 .AddLabel("Other Turn", nameof(PaydayMainViewModel.OtherLabel))
                 .AddLabel("Progress", nameof(PaydayMainViewModel.MonthLabel))
                 .AddLabel("Status", nameof(PaydayMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Money", true, nameof(PaydayPlayerItem.MoneyHas), category: EnumScoreSpecialCategory.Currency)
                .AddColumn("Loans", true, nameof(PaydayPlayerItem.Loans), category: EnumScoreSpecialCategory.Currency);
            base.OnInitialized();
        }
        private string GetColor => _graphicsData!.GameContainer.SingleInfo!.Color.ToColor();
    }
}