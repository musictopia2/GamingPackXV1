using BasicBlazorLibrary;
using BasicBlazorLibrary.Components.Basic;
using BasicBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.CollectionClasses;
using MinesweeperCP.ViewModels;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace MinesweeperBlazor.Views
{
    public partial class MinesweeperMainView
    {
        //any code needed will go here.

        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private static string GetMethodName => nameof(MinesweeperMainViewModel.ChangeFlag);

        private string GetDisplay()
        {
            if (DataContext!.IsFlagging)
            {
                return "Flag Mines";
            }
            return "Unflip Mines";
        }
        private string BackgroundColor()
        {
            if (DataContext!.IsFlagging)
            {
                return cc.Yellow;
            }
            return cc.Aqua;
        }

        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Mines Needed", nameof(MinesweeperMainViewModel.HowManyMinesNeeded))
                .AddLabel("Mines Left", nameof(MinesweeperMainViewModel.NumberOfMinesLeft))
                .AddLabel("Level Chosen", nameof(MinesweeperMainViewModel.LevelChosen));
            base.OnInitialized();
        }
    }
}