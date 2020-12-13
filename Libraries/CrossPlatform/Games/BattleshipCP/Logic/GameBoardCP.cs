using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MiscProcesses;
using BattleshipCP.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Collections.Generic;
using System.Linq;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BattleshipCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardCP
    {
        public Dictionary<int, string>? RowList;
        public Dictionary<int, string>? ColumnList; //has to be string if possible
        public BattleshipCollection? HumanList;
        public BattleshipCollection? ComputerList;
        public float SpaceSize { get; set; }
        private bool _isWaiting;
        public void StartGame()
        {
            _isWaiting = false;
            foreach (var item in HumanList!)
            {
                item.FillColor = cs.Blue; //i think this is all that it needs to when it starts game.
            }

        }
        public void HumanWaiting()
        {
            if (_isWaiting == true)
            {
                return;
            }
            HumanList!.PlayerWaiting();
            _isWaiting = true;
        }
        public void ClearBoard()
        {
            HumanList!.Clear();
            ComputerList!.Clear(); //i think both this time.
        }
        public GameBoardCP()
        {
            PrivateInit(); //if i am wrong, rethink.
        }
        private void PrivateInit()
        {
            CustomBasicList<string> tempList = new CustomBasicList<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "I",
                "J"
            };
            RowList = new Dictionary<int, string>();
            var loopTo = tempList.Count;
            int x;
            for (x = 1; x <= loopTo; x++)
            {
                RowList.Add(RowList.Count + 1, tempList[x - 1]);// because 0 based
            }
            ColumnList = new Dictionary<int, string>();
            for (x = 1; x <= 10; x++)
            {
                ColumnList.Add(ColumnList.Count + 1, x.ToString());
            }
            int y = 0;
            HumanList = new BattleshipCollection();
            ComputerList = new BattleshipCollection();
            x = 0;
            foreach (var thisRow in RowList.Values)
            {
                x += 1;
                foreach (var thisColumn in ColumnList.Values)
                {
                    y += 1;
                    HumanList[x, int.Parse(thisColumn)].Letter = thisRow.ToLower();
                    ComputerList[x, int.Parse(thisColumn)].Letter = thisRow.ToLower();
                }
            }
        }
        public bool CanChooseSpace(Vector space)
        {
            return HumanList![space].Hit == EnumWhatHit.None;
        }
        public void MarkField(Vector space, EnumWhatHit hit)
        {
            FieldInfoCP thisField = HumanList![space];
            thisField.Hit = hit;
            if (hit == EnumWhatHit.Miss)
            {
                thisField.FillColor = cs.Lime;
            }
        }
        public void PlaceShip(Vector space, int nextShip, out string label)
        {
            FieldInfoCP thisField;
            thisField = HumanList![space];
            thisField.FillColor = cs.Gray;
            thisField.ShipNumber = nextShip;
            label = $"{thisField.Letter.ToUpper()}:{space.Column}";
        }
        public bool HumanWon()
        {
            return HumanList.Count(items => items.Hit == EnumWhatHit.Hit) == 16;
        }
    }
}