using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using CommonBasicStandardLibraries.CollectionClasses;
using FluxxCP.Containers;
using FluxxCP.Data;
using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace FluxxBlazor.Views
{
    public partial class FluxxMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private FluxxVMData? _vmData;
        private FluxxGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<FluxxVMData>();
            _gameContainer = cons.Resolve<FluxxGameContainer>();
            _scores.Clear();
            _scores.AddColumn("# In Hand", false, nameof(FluxxPlayerItem.ObjectCount))
                .AddColumn("# Of Keepers", false, nameof(FluxxPlayerItem.NumberOfKeepers))
                .AddColumn("Bread", false, nameof(FluxxPlayerItem.Bread), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Chocolate", false, nameof(FluxxPlayerItem.Chocolate), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Cookies", false, nameof(FluxxPlayerItem.Cookies), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Death", false, nameof(FluxxPlayerItem.Death), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Dreams", false, nameof(FluxxPlayerItem.Dreams), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Love", false, nameof(FluxxPlayerItem.Love), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Milk", false, nameof(FluxxPlayerItem.Milk), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Money", false, nameof(FluxxPlayerItem.Money), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Peace", false, nameof(FluxxPlayerItem.Peace), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Sleep", false, nameof(FluxxPlayerItem.Sleep), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Television", false, nameof(FluxxPlayerItem.Television), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("The Brain", false, nameof(FluxxPlayerItem.TheBrain), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("The Moon", false, nameof(FluxxPlayerItem.TheMoon), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("The Rocket", false, nameof(FluxxPlayerItem.TheRocket), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("The Sun", false, nameof(FluxxPlayerItem.TheSun), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("The Toaster", false, nameof(FluxxPlayerItem.TheToaster), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Time", false, nameof(FluxxPlayerItem.Time), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("War", false, nameof(FluxxPlayerItem.War), category: EnumScoreSpecialCategory.TrueFalse);
            _labels.Clear();
            _labels.AddLabel("Plays Left", nameof(FluxxMainViewModel.PlaysLeft))
                .AddLabel("Hand Limit", nameof(FluxxMainViewModel.HandLimit))
                .AddLabel("Keeper Limit", nameof(FluxxMainViewModel.KeeperLimit))
                .AddLabel("Play Limit", nameof(FluxxMainViewModel.PlayLimit))
                .AddLabel("Another Turn", nameof(FluxxMainViewModel.AnotherTurn))
                .AddLabel("Current Turn", nameof(FluxxMainViewModel.NormalTurn))
                .AddLabel("Other Turn", nameof(FluxxMainViewModel.OtherTurn))
                .AddLabel("Status", nameof(FluxxMainViewModel.Status))
                .AddLabel("Draw Bonus", nameof(FluxxMainViewModel.DrawBonus))
                .AddLabel("Play Bonus", nameof(FluxxMainViewModel.PlayBonus))
                .AddLabel("Cards Drawn", nameof(FluxxMainViewModel.CardsDrawn))
                .AddLabel("Cards Played", nameof(FluxxMainViewModel.CardsPlayed))
                .AddLabel("Draw Rules", nameof(FluxxMainViewModel.DrawRules))
                .AddLabel($"Previous {Constants.vbCrLf}Bonus", nameof(FluxxMainViewModel.PreviousBonus));
            base.OnInitialized();
        }
        private string EndMethod => nameof(FluxxMainViewModel.EndTurnAsync);
        private string DiscardMethod => nameof(FluxxMainViewModel.DiscardAsync);
        private string UnselectMethod => nameof(FluxxMainViewModel.UnselectHandCards);
        private string SelectMethod => nameof(FluxxMainViewModel.SelectHandCards);
        private string GetColumns => "65vw 35vw";
    }
}