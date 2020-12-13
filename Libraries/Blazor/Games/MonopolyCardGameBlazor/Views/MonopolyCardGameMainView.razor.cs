using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using MonopolyCardGameCP.Data;
using MonopolyCardGameCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace MonopolyCardGameBlazor.Views
{
    public partial class MonopolyCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private MonopolyCardGameVMData? _vmData;
        private MonopolyCardGameGameContainer? _gameContainer;
        private CustomBasicList<MonopolyCardGamePlayerItem> _players = new CustomBasicList<MonopolyCardGamePlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<MonopolyCardGameVMData>();
            _gameContainer = cons.Resolve<MonopolyCardGameGameContainer>();
            _players = _gameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(MonopolyCardGameMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(MonopolyCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(MonopolyCardGamePlayerItem.ObjectCount))
                .AddColumn("Previous Money", true, nameof(MonopolyCardGamePlayerItem.PreviousMoney), category: EnumScoreSpecialCategory.Currency)
                .AddColumn("Total Money", true, nameof(MonopolyCardGamePlayerItem.TotalMoney), category: EnumScoreSpecialCategory.Currency);
            base.OnInitialized();
        }
        private string ResumeMethod => nameof(MonopolyCardGameMainViewModel.ResumeAsync);
        private string OutMethod => nameof(MonopolyCardGameMainViewModel.GoOutAsync);
    }
}