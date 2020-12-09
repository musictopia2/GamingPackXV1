using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using MinesweeperCP.Data;
using MinesweeperCP.Logic;
using MinesweeperCP.ViewModels;
using System;
using System.Linq;
using aa = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace MinesweeperBlazor.Views
{
    public partial class GameboardBlazor
    {
        [CascadingParameter]
        public MinesweeperMainViewModel? DataContext { get; set; }
        [Parameter]
        public Action? StateChanged { get; set; }
        private string MethodName => nameof(MinesweeperMainViewModel.MakeMoveAsync);

        private MinesweeperMainGameClass MainGame { get; set; }

        public GameboardBlazor()
        {
            MainGame = aa.Resolve<MinesweeperMainGameClass>();
        }


        private CustomBasicList<MineSquareModel> SquareList => MainGame.GetSquares();
        public string GetViewHeight()
        {
            double totalNeeded = 97 / MainGame.NumberOfRows;
            return $"{totalNeeded}vh";
        }

        private MineSquareModel GetSquare(int row, int column)
        {
            return SquareList.Single(x => x.Row == row && x.Column == column);
        }
    }
}