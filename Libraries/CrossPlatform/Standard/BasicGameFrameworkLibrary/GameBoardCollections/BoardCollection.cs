using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using static BasicGameFrameworkLibrary.DIContainers.Helpers;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.GameBoardCollections
{
    public class BoardCollection<C> : IEnumerable<C>, IAdvancedDIContainer, IBoardCollection<C> where C : class, IBasicSpace, new() //so this can go ahead and create a new one.
    {
        private readonly Dictionary<Vector, C> _privateDict = new Dictionary<Vector, C>();
        public IConsole? ConsoleInfo;
        public IGamePackageResolver? MainContainer { get; set; }
        public Func<C, object>? MainObjectSelector;
        public Func<C, char>? BoardResultSelector; //that way if i am using for connect four, has to figure out what the string will be that is one character.
        public BoardCollection() { }
        public BoardCollection(IEnumerable<C> previousBoard)
        {
            _privateDict = previousBoard.ToDictionary(Items => Items.Vector);
            _howManyColumns = _privateDict.Values.Max(Items => Items.Vector.Column);
            _howManyRows = _privateDict.Values.Max(Items => Items.Vector.Row);
        }
        private void CheckResolver()
        {
            PopulateContainer(this);
        }
        public void PrintBoard()
        {
            if (ConsoleInfo == null)
            {
                CheckResolver();
                ConsoleInfo = MainContainer!.Resolve<IConsole>("");
            }
            if (ConsoleInfo == null)
            {
                throw new BasicBlankException("Was unable to resolve console for printing");
            }
            StringBuilder thisStr = new StringBuilder();
            bool extras;
            extras = ConsoleInfo.ExtraSpaces;
            if (extras == true)
                thisStr.Append(" ");
            thisStr.Append("   |");
            for (int i = 0; i < _howManyColumns; i++)
            {
                thisStr.Append($" {i + 1} |");
            }
            ConsoleInfo.WriteLine(thisStr.ToString());
            for (int row = 0; row < _howManyRows; row++)
            {
                thisStr = new StringBuilder();
                if (extras == false)
                {
                    thisStr.Append($" {row + 1} | ");
                }
                else
                {
                    thisStr.Append($" {row + 1} | ");
                }
                for (int column = 0; column < _howManyColumns; column++)
                {
                    C thisC = this[row + 1, column + 1];
                    if (thisC.IsFilled() == true) //maybe it could be a property. (readonly property)
                    {
                        thisStr.Append(BoardResultSelector!.Invoke(thisC));
                    }
                    else
                    {
                        thisStr.Append("  ");
                    }
                    thisStr.Append(" | ");
                }
                ConsoleInfo.WriteLine(thisStr.ToString());
            }
            ConsoleInfo.WriteLine(""); //so it can separate them
        }
        public BoardCollection(int rows, int columns)
        {
            SetDimensions(rows, columns);
        }
        public int GetTotalRows()
        {
            return _howManyRows;
        }
        public int GetTotalColumns()
        {
            return _howManyColumns;
        }
        private Vector GetVector(int row, int column)
        {
            Vector thisV = new Vector(row, column);
            return thisV;
        }
        public void ForEach(Action<C> action)
        {
            foreach (var item in _privateDict.Values)
            {
                action.Invoke(item);
            }
        }
        public void PerformActionOnConditional(Predicate<C> predicate, Action<C> action)
        {
            foreach (var item in _privateDict.Values)
            {
                if (predicate(item) == true)
                {
                    action.Invoke(item);
                }
            }
        }
        public async Task ForEachAsync(ActionAsync<C> action) //i think done.
        {
            foreach (C thisItem in _privateDict.Values)
            {
                await action.Invoke(thisItem);
            }
        }
        public CustomBasicList<CustomBasicList<C>> GetPossibleCombinations(int howManyNeeded) //will not take in a condition for this.  too complex otherwise.
        {
            if (howManyNeeded <= 0 || howManyNeeded > _howManyColumns || howManyNeeded > _howManyRows)
            {
                throw new CustomArgumentException(nameof(howManyNeeded), "The number needed can't be less than one or more than rows and columns you have");
            }
            int currentC;
            int currentR;
            void ResetRowColumns()
            {
                currentC = 1;
                currentR = 1;
            }
            ResetRowColumns();
            CustomBasicList<CustomBasicList<C>> output = new CustomBasicList<CustomBasicList<C>>();
            CustomBasicList<CustomBasicList<C>> temps;
            temps = GetSpecificCombo(currentR, currentC, EnumTempInfo.Horizontal, howManyNeeded);
            output.AddRange(temps);
            ResetRowColumns();
            temps = GetSpecificCombo(currentR, currentC, EnumTempInfo.Vertical, howManyNeeded);
            output.AddRange(temps);
            ResetRowColumns();
            temps = GetSpecificCombo(currentR, currentC, EnumTempInfo.DiagRightH, howManyNeeded);
            output.AddRange(temps);
            currentC = _howManyColumns;
            temps = GetSpecificCombo(currentR, currentC, EnumTempInfo.DiagLeftH, howManyNeeded);
            output.AddRange(temps);
            return output;
        }
        private CustomBasicList<CustomBasicList<C>> GetSpecificCombo(int startR, int startC, EnumTempInfo direction, int howMany, bool possibleDup = false)
        {
            CustomBasicList<CustomBasicList<C>> output = new CustomBasicList<CustomBasicList<C>>();
            int currentR;
            int currentC;
            currentC = startC;
            currentR = startR;
            EnumDirection real;
            switch (direction)
            {
                case EnumTempInfo.None:
                    throw new BasicBlankException("None not supported");
                case EnumTempInfo.Horizontal:
                    real = EnumDirection.Horizontal;
                    break;
                case EnumTempInfo.Vertical:
                    real = EnumDirection.Vertical;
                    break;
                case EnumTempInfo.DiagRightH:

                case EnumTempInfo.DiagRightV:
                    real = EnumDirection.DiagRight;
                    break;
                case EnumTempInfo.DiagLeftH:

                case EnumTempInfo.DiagLeftV:
                    real = EnumDirection.DiagLeft;
                    break;
                default:
                    throw new BasicBlankException("Not Supported");
            }
            int x = 0;
            CustomBasicList<C> temps;
            do
            {
                x++;
                if (x > 1 || possibleDup == false)
                {
                    temps = GetSpecificVectors(currentR, currentC, real, howMany);
                    if (temps.Count == howMany)
                    {
                        output.Add(temps); //so it can try other combos.
                    }
                }
                switch (direction)
                {
                    case EnumTempInfo.None:
                        break;
                    case EnumTempInfo.Horizontal:
                        currentR++;
                        if (currentR > _howManyRows)
                        {
                            currentC++;
                            currentR = startR;
                        }
                        if (currentC > _howManyColumns)
                        {
                            return output;
                        }
                        break;
                    case EnumTempInfo.DiagRightH:
                        currentR++;
                        if (currentR > _howManyRows)
                        {
                            currentC++;
                            currentR = startR;
                        }
                        if (currentC > _howManyColumns)
                        {
                            return output;
                        }
                        break;
                    case EnumTempInfo.DiagLeftH:
                        currentC--;

                        if (currentC == 0)
                        {
                            currentC = startC;
                            currentR++;
                        }
                        if (currentR > _howManyRows)
                        {
                            return output;
                        }
                        break;
                    case EnumTempInfo.Vertical:
                        currentC++;
                        if (currentC > _howManyColumns)
                        {
                            currentR++;
                            currentC = startC;
                        }
                        if (currentR > _howManyRows)
                            return output;
                        break;
                    case EnumTempInfo.DiagRightV:

                    case EnumTempInfo.DiagLeftV:
                        currentC++;
                        if (currentC > _howManyColumns)
                        {
                            return output;
                        }
                        break;
                    default:
                        break;
                }

            } while (true);
        }
        public CustomBasicList<C> GetWinCombo(CustomBasicList<CustomBasicList<C>> comboList)
        {
            foreach (var currentCombo in comboList)
                if (currentCombo.All(temps => temps.IsFilled()) == true)
                {
                    object searchObject = MainObjectSelector!.Invoke(currentCombo.First());
                    bool allTrue = true;
                    object compareObject;
                    foreach (var item in currentCombo)
                    {
                        compareObject = MainObjectSelector.Invoke(item);
                        if (compareObject.Equals(searchObject) == false) //try this way.
                        {
                            allTrue = false;
                            break;
                        }
                    }
                    if (allTrue == true)
                    {
                        return currentCombo;
                    }
                };
            return new CustomBasicList<C>();
        }
        public CustomBasicList<C> GetEmptySpaces()
        {
            return _privateDict.Values.Where(items => items.IsFilled() == false).ToCustomBasicList();
        }
        public CustomBasicList<C> GetAlmostWinList(CustomBasicList<CustomBasicList<C>> comboList)
        {
            CustomBasicList<C> output = new CustomBasicList<C>();
            bool allTrue;
            foreach (var currentCombo in comboList)
            {
                int needs;
                needs = currentCombo.Count - 1;
                int actual = currentCombo.Count(Items => Items.IsFilled());
                if (needs == actual)
                {
                    object? searchObject = null;
                    object? compareObject;
                    C? fillItem = null;
                    allTrue = true;
                    foreach (var item in currentCombo)
                        if (item.IsFilled() == false)
                        {
                            fillItem = item;
                        }
                        else if (searchObject == null)
                        {
                            searchObject = MainObjectSelector!.Invoke(item);
                        }
                        else
                        {
                            compareObject = MainObjectSelector!.Invoke(item);
                            if (compareObject.Equals(searchObject) == false) //try this way.
                            {
                                allTrue = false;
                                break;
                            }
                        }
                    if (fillItem == null)
                    {
                        throw new BasicBlankException("There was no fill item.  Rethink");
                    }
                    if (allTrue == true)
                    {
                        output.Add(fillItem);
                    }
                }
            }
            return output;
        }
        public bool IsFilled(Vector thisV)
        {
            C thisS = this[thisV];
            return thisS.IsFilled(); //since i implement the interface.
        }
        public bool IsFilled(int row, int column)
        {
            C thisS = this[row, column];
            return thisS.IsFilled();
        }
        public bool DidWin(CustomBasicList<CustomBasicList<C>> comboList)
        {
            foreach (var currentCombo in comboList)
                if (currentCombo.All(Temps => Temps.IsFilled()) == true)
                {
                    object searchObject = MainObjectSelector!.Invoke(currentCombo.First());
                    bool allTrue = true;
                    object compareObject;
                    foreach (var item in currentCombo)
                    {
                        compareObject = MainObjectSelector.Invoke(item);
                        if (compareObject.Equals(searchObject) == false) //try this way.
                        {
                            allTrue = false;
                            break;
                        }
                    }
                    if (allTrue == true)
                    {
                        return true;
                    }
                };
            return false; //if you can't prove a win, then you did not win.
        }
        public bool IsAllFilled()
        {
            return _privateDict.Values.All(items => items.IsFilled() == true);
        }
        public CustomBasicList<C> GetAllColumns(int row)
        {
            return GetSpecificVectors(row, 1, EnumDirection.Horizontal, _howManyRows);
        }
        public CustomBasicList<C> GetAllRows(int column) //gets all rows for that column
        {
            return GetSpecificVectors(1, column, EnumDirection.Vertical, _howManyColumns);
        }
        public CustomBasicList<C> GetSpecificVectors(int row, int column, EnumDirection direction, int count, Predicate<C>? predicate = null)
        {
            Check();
            if (column > _howManyColumns || row > _howManyRows)
            {
                throw new BasicBlankException("Out of bounds for getting specific vectors");
            }
            if (column == 0)
            {
                throw new BasicBlankException("Column cannot be 0 When Getting Vectors");
            }
            CustomBasicList<C> output = new CustomBasicList<C>();
            int currentR;
            int currentC;
            currentC = column;
            currentR = row;
            for (int i = 0; i < count; i++)
            {
                Vector thisV;
                try
                {
                    thisV = GetVector(currentR, currentC);
                    if (predicate == null)
                    {
                        output.Add(_privateDict[thisV]);
                    }
                    else if (predicate.Invoke(_privateDict[thisV]) == true)
                    {
                        output.Add(_privateDict[thisV]);
                    }
                }
                catch (Exception ex)
                {
                    throw new BasicBlankException($"Error.  Row Trying Is {row} And Column Is {column}.  Message Was {ex.Message}");
                }
                switch (direction)
                {
                    case EnumDirection.None:
                        throw new BasicBlankException("No direction specified");
                    case EnumDirection.Horizontal:
                        currentC++;
                        break;
                    case EnumDirection.Vertical:
                        currentR++;
                        break;
                    case EnumDirection.DiagRight:
                        currentC++;
                        currentR++;
                        break;
                    case EnumDirection.DiagLeft:
                        currentC--;
                        currentR++;
                        break;
                    default:
                        throw new BasicBlankException("Not supported");
                }
                if (currentC > _howManyColumns || currentR > _howManyRows || currentC <= 0 || currentR <= 0)
                {
                    return output;
                }
            }
            return output;
        }
        private int _howManyRows;
        private int _howManyColumns;
        public C this[int row, int column]
        {
            get
            {
                Check();
                Vector ThisGrid = GetVector(row, column);
                return _privateDict[ThisGrid];
            }
        }
        public C this[Vector thisV]
        {
            get
            {
                Check();
                return _privateDict[thisV];
            }
        }
        private void Check()
        {
            if (_privateDict.Count == 0)
            {
                throw new BasicBlankException("You have to have at least one item for your dictionary");
            }
        }
        public void SetDimensions(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new BasicBlankException("The rows and columns must be greater than 0");
            }
            _howManyRows = rows;
            _howManyColumns = columns;
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    Vector thisGrid = new Vector(x + 1, y + 1);
                    C thisC = new C();
                    thisC.Vector = thisGrid;
                    _privateDict[thisGrid] = thisC;
                }
            }
                
        }
        public void Clear()
        {
            Check();
            foreach (var thisItem in _privateDict.Values) //you can't really remove the spaces.  what you do instead is clear it.
            {
                thisItem.ClearSpace();
            }
        }
        public IEnumerator<C> GetEnumerator()
        {
            return _privateDict.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _privateDict.Values.GetEnumerator();
        }
    }
}