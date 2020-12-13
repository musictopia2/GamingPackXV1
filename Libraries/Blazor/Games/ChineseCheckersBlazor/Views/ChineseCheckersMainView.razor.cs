using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using ChineseCheckersCP.Data;
using ChineseCheckersCP.Logic;
using ChineseCheckersCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace ChineseCheckersBlazor.Views
{




    public partial class ChineseCheckersMainView
    {

        

        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardGraphicsCP? _graphics;
        private ChineseCheckersGameContainer? _container;
        protected override void OnInitialized()
        {
            _graphics = cons!.Resolve<GameBoardGraphicsCP>();
            _container = cons.Resolve<ChineseCheckersGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ChineseCheckersMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(ChineseCheckersMainViewModel.Instructions))
                 .AddLabel("Status", nameof(ChineseCheckersMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(ChineseCheckersMainViewModel.EndTurnAsync);
    }
}