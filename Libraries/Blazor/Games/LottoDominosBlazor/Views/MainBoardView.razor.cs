using LottoDominosCP.Logic;
using LottoDominosCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace LottoDominosBlazor.Views
{
    public partial class MainBoardView
    {
        [CascadingParameter]
        public MainBoardViewModel? DataContext { get; set; }
        private GameBoardCP? Board { get; set; }
        protected override void OnInitialized()
        {
            Board = Resolve<GameBoardCP>(); //best way to handle this.
            base.OnInitialized();
        }
    }
}