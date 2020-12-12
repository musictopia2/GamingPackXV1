using BasicGameFrameworkLibrary.GameBoardCollections;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace BingoCP.Data
{
    public class GameBoardCP : IBoardCollection<SpaceInfoCP>
    {
        private readonly BoardCollection<SpaceInfoCP> _privateBoard;
        public GameBoardCP()
        {
            _privateBoard = new BoardCollection<SpaceInfoCP>(6, 5); //because we have header.  hopefully that works.
        }
        public GameBoardCP(IEnumerable<SpaceInfoCP> previousList)
        {
            _privateBoard = new BoardCollection<SpaceInfoCP>(previousList);
        }
        private bool _didInit;
        public SpaceInfoCP this[int row, int column] => _privateBoard[row, column];
        public SpaceInfoCP this[Vector thisV] => _privateBoard[thisV];
        public void LoadBoard()
        {
            if (_didInit)
            {
                return;
            }
            var list = _privateBoard.Where(x => x.Vector.Row == 1).ToCustomBasicList();
            list.ForEach(x => x.IsEnabled = false);
            _privateBoard[4, 2].Text = "Free";
            _didInit = true;
        }
        public void ClearBoard(BingoVMData model)
        {
            _privateBoard.Clear();
            model.NumberCalled = "";
        }
        public int GetTotalColumns()
        {
            return _privateBoard.GetTotalColumns();
        }

        public int GetTotalRows()
        {
            return _privateBoard.GetTotalRows();
        }
        public IEnumerator<SpaceInfoCP> GetEnumerator()
        {
            return _privateBoard.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _privateBoard.GetEnumerator();
        }
    }
}