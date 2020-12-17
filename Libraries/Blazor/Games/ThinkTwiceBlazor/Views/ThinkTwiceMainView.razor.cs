using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ThinkTwiceCP.Data;
using ThinkTwiceCP.Logic;
using ThinkTwiceCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace ThinkTwiceBlazor.Views
{
    public partial class ThinkTwiceMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private readonly string _diceHeight = "15vh";
        private ThinkTwiceGameContainer? _gameContainer;
        private CategoriesDice? _categories;
        private Multiplier? _multiplier;
        protected override void OnInitialized()
        {
            _categories = cons!.Resolve<CategoriesDice>();
            _multiplier = cons.Resolve<Multiplier>();
            _gameContainer = cons.Resolve<ThinkTwiceGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ThinkTwiceMainViewModel.NormalTurn))
                .AddLabel("Roll", nameof(ThinkTwiceMainViewModel.RollNumber))
                .AddLabel("Category", nameof(ThinkTwiceMainViewModel.CategoryChosen))
                .AddLabel("Score", nameof(ThinkTwiceMainViewModel.Score))
                .AddLabel("Status", nameof(ThinkTwiceMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Score Round", true, nameof(ThinkTwicePlayerItem.ScoreRound))
                .AddColumn("Score Game", true, nameof(ThinkTwicePlayerItem.ScoreGame));
            base.OnInitialized();
        }
        private string RollMethod => nameof(ThinkTwiceMainViewModel.RollDiceAsync);
        private string EndMethod => nameof(ThinkTwiceMainViewModel.EndTurnAsync);
        private string MultMethod => nameof(ThinkTwiceMainViewModel.RollMultAsync);
        private async Task CategoryClickedAsync()
        {
            if (_categories!.Visible == false)
            {
                return;
            }
            await _gameContainer!.CategoryClicked!.Invoke(); //try this way.  because it already shows its processing now.  hopefully that fixes the problem.
            //await _gameContainer.ProcessCustomCommandAsync(_gameContainer.CategoryClicked!);
        }
    }
}