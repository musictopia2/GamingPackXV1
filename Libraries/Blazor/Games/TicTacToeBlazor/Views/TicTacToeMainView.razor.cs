using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using TicTacToeCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using TicTacToeCP.Data;

namespace TicTacToeBlazor.Views
{
    public partial class TicTacToeMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private TicTacToeSaveInfo? _save;
        private WinInfo? _win;
        protected override void OnInitialized()
        {
            _labels.Clear();
            DataContext!.CommandContainer.AddAction(ShowChange);
            _labels.AddLabel("Turn", nameof(TicTacToeMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(TicTacToeMainViewModel.Status));
            base.OnInitialized();
        }
        private string MethodName => nameof(TicTacToeMainViewModel.MakeMoveAsync);
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        private void GetWin()
        {
            _save = cons!.Resolve<TicTacToeSaveInfo>();
            _win = _save.GameBoard.GetWin();
        }

        private string GetText(SpaceInfoCP space)
        {
            switch (space.Status)
            {
                case EnumSpaceType.Blank:
                    return "";
                case EnumSpaceType.O:
                    return "O";
                case EnumSpaceType.X:
                    return "X";
                default:
                    return "";
            }
        }
    }
}