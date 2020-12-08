using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.Views
{
    public partial class YahtzeeMainView<D>
        where D : SimpleDice, new()
    {

        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();

        private IEventAggregator? _aggregator;
        private int _bottomDescriptionWidth;
        protected override void OnInitialized()
        {
            _aggregator = cons!.Resolve<IEventAggregator>();
            _aggregator.Subscribe(this);
            _labels.Clear();
            IYahtzeeStyle yahtzeeStyle = cons.Resolve<IYahtzeeStyle>();
            _bottomDescriptionWidth = yahtzeeStyle.BottomDescriptionWidth;
            DataContext!.CommandContainer.AddAction(ShowChange);
            _labels.AddLabel("Turn", nameof(YahtzeeMainViewModel<D>.NormalTurn))
                .AddLabel("Roll", nameof(YahtzeeMainViewModel<D>.RollNumber))
                .AddLabel("Status", nameof(YahtzeeMainViewModel<D>.Status))
                .AddLabel("Turn #", nameof(YahtzeeMainViewModel<D>.Round));
            _scores.Clear();
            _scores.AddColumn("Points", false, nameof(YahtzeePlayerItem<D>.Points));
            base.OnInitialized();
        }
        private string RollMethod => nameof(YahtzeeMainViewModel<D>.RollDiceAsync);

        private ScoreContainer GetContainer()
        {
            return cons!.Resolve<ScoreContainer>();
        }
    }
}