using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SnakesAndLaddersCP.Data;
using SnakesAndLaddersCP.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace SnakesAndLaddersBlazor
{

    public partial class GameBoardBlazor
    {
        [CascadingParameter]
        public SnakesAndLaddersMainViewModel? DataContext { get; set; }
        [Parameter]
        public CustomBasicList<SnakesAndLaddersPlayerItem>? PlayerList { get; set; }
        private CustomBasicList<SnakesAndLaddersPlayerItem> ModifiedList
        {
            get
            {
                var firstList = PlayerList!.ToCustomBasicList();
                firstList.RemoveAllOnly(x => x.SpaceNumber == 0);
                var nextList = firstList.GroupBy(x => x.SpaceNumber);
                CustomBasicList<SnakesAndLaddersPlayerItem> output = new CustomBasicList<SnakesAndLaddersPlayerItem>();
                foreach (var item in nextList)
                {
                    output.Add(item.Last());
                }
                return output;
            }
        }
        private class TempSpace
        {
            public RectangleF Bounds { get; set; }
            public string Fill { get; set; } = ""; //i think.
        }
        private readonly Dictionary<int, TempSpace> _spaceList = new Dictionary<int, TempSpace>();
        private Assembly GetAssembly => Assembly.GetAssembly(GetType());
        protected override void OnInitialized()
        {
            _spaceList.Clear();
            LoadSpaces();
            base.OnInitialized();
        }
        private async Task SpaceClicked(int number)
        {
            if (DataContext!.CommandContainer.CanExecuteManually() == false)
            {
                return;
            }
            if (DataContext.CanMakeMove(number) == false)
            {
                return;
            }
            await DataContext.CommandContainer.ProcessCustomCommandAsync(DataContext.MakeMoveAsync, number); //try this way.
        }
        private void LoadSpaces()
        {
            RectangleF bounds = new RectangleF(0, 0, 500, 500);
            int int_RowCount;
            for (int_RowCount = 1; int_RowCount <= 10; int_RowCount++)
            {
                int int_ColCount;
                for (int_ColCount = 1; int_ColCount <= 10; int_ColCount++)
                {
                    TempSpace thisExp = new TempSpace();
                    int int_Count;
                    if ((int_RowCount % 2) == 0)
                    {
                        int_Count = (100 - (((int_RowCount - 1) * 10) + (11 - int_ColCount))) + 1;
                    }
                    else
                    {
                        int_Count = (100 - (((int_RowCount - 1) * 10) + (int_ColCount))) + 1;
                    }
                    // *** If it's an even row, number it backwards
                    if (int_Count == 100)
                    {
                        thisExp.Fill = cc.DarkRed;
                    }
                    else if ((int_Count % 2) == 0)
                    {
                        thisExp.Fill = cc.Gold;
                    }
                    else
                    {
                        thisExp.Fill = cc.DodgerBlue;
                    }
                    thisExp.Bounds = new RectangleF(bounds.Location.X + ((bounds.Width * (int_ColCount - 1)) / 10), bounds.Location.Y + ((bounds.Height * (int_RowCount - 1)) / 10), ((bounds.Width * int_ColCount) / 10) - ((bounds.Width * (int_ColCount - 1)) / 10), ((bounds.Height * int_RowCount) / 10) - ((bounds.Height * (int_RowCount - 1)) / 10));
                    _spaceList.Add(int_Count, thisExp);
                }
            }
        }
    }
}