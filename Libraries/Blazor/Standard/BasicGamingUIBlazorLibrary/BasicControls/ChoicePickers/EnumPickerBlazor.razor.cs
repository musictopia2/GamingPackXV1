using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.GamePieceModels;
using Microsoft.AspNetCore.Components;
using System;
namespace BasicGamingUIBlazorLibrary.BasicControls.ChoicePickers
{
    public partial class EnumPickerBlazor<E>
        where E : struct, Enum
    {
        [Parameter]
        public SimpleEnumPickerVM<E>? ListPicker { get; set; }

        [Parameter]
        public int Rows { get; set; } = 1;
        [Parameter]
        public int Columns { get; set; }

        [Parameter]
        public string TargetSize { get; set; } = "25vh"; //can be adjustable as needed.

        [Parameter]
        public RenderFragment<BasicPickerData<E>>? ChildContent { get; set; }

        private int TotalRows()
        {
            if (Columns == 0 && Rows == 1)
            {
                return 1;
            }
            if (Columns == 1 && Rows == 1)
            {
                return ListPicker!.ItemList.Count;
            }
            var (_, rows) = SmartRowsColumns();
            return rows;
        }

        private (int columns, int rows) SmartRowsColumns()
        {
            int c;
            int r;
            c = 0;
            r = 0;
            int mc = 0;
            int mr = 0;
            if (Columns < 2)
            {
                mc = 1;
            }
            if (Rows < 2)
            {
                mr = 1;
            }
            foreach (var item in ListPicker!.ItemList)
            {
                if (Columns > 1)
                {
                    c++;
                    if (c > mc)
                    {
                        mc = c;
                    }

                    if (c > Columns)
                    {
                        c = 0;
                        r++;
                        mr = r;
                    }
                }
                else
                {
                    r++;
                    if (r > mr)
                    {
                        mr = r;
                    }
                    if (r > Rows)
                    {
                        r = 0;
                        c++;
                        mc = c;
                    }
                }
            }

            return (mc, mr);
        }

        private int TotalColumns()
        {
            if (Columns == 0 && Rows == 1)
            {
                return ListPicker!.ItemList.Count;
            }
            if (Columns == 1 && Rows == 1)
            {
                return 1; //has to have at least 1.
            }
            var (columns, _) = SmartRowsColumns();
            return columns;
        }
    }
}