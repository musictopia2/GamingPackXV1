using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BattleshipCP.Data;
using BattleshipCP.Logic;
using BattleshipCP.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BattleshipBlazor.Views
{
    public partial class BattleshipMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private CustomBasicList<string> _rowList = new CustomBasicList<string>();
        private CustomBasicList<string> _columnList = new CustomBasicList<string>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            DataContext!.CommandContainer.AddAction(ShowChange);
            _labels.AddLabel("Turn", nameof(BattleshipMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(BattleshipMainViewModel.Status));
            base.OnInitialized();
        }
        private CustomBasicList<ShipInfoCP>? _ships;
        private BattleshipCollection? _humanList;
        protected override void OnParametersSet()
        {
            var ship = cons!.Resolve<ShipControlCP>(); //i think.
            _ships = ship.ShipList.Values.ToCustomBasicList();
            GameBoardCP gameBoard = cons.Resolve<GameBoardCP>();
            _humanList = gameBoard.HumanList!;
            _rowList = gameBoard.RowList!.Values.ToCustomBasicList();
            _columnList = gameBoard.ColumnList!.Values.ToCustomBasicList();
            base.OnParametersSet();
        }
        private string ColumnText => "55vw 45vw"; //could adjust as needed.
        private string PositionMethod => nameof(BattleshipMainViewModel.ShipDirection);
        private string Color(bool horizontal)
        {
            if (DataContext!.ShipsHorizontal == horizontal)
            {
                return cc.Yellow;
            }
            return cc.Aqua;
        }
        private string SpaceMethod => nameof(BattleshipMainViewModel.MakeMoveAsync);
    }
}