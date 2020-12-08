using BasicGameFrameworkLibrary.ChooserClasses;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.ChoicePickers
{
    public partial class ListChooserBlazor
    {
        /// <summary>
        /// should be defined by view height.  if 5 is set, then no matter what, 20 would be displayed vertically.
        /// </summary>
        [Parameter]
        public string TextHeight { get; set; } = "7vh"; //default to 7 view height but can vary as needed.

        [Parameter]
        public int TextWidth { get; set; } = 100;

        [Parameter]
        public int TotalColumns { get; set; }

        [Parameter]
        public IListViewPicker? ListPicker { get; set; }
        [Parameter]
        public string TextColor { get; set; } = cc.Navy;
        [Parameter]
        public bool CanHighlight { get; set; } = true;

        private int DisplayColumns()
        {
            if (TotalColumns == 0)
            {
                return 1;
            }
            if (TotalColumns <= ListPicker!.TextList.Count)
            {
                return TotalColumns;
            }
            return ListPicker.TextList.Count;
        }

        private int TotalRows(int total) //this is useful for more than one occasion.
        {
            if (TotalColumns == 0)
            {
                return total;
            }
            int x = 0;
            int upTo = 0;
            do
            {
                x += TotalColumns;
                upTo++;
                if (x >= total)
                {
                    return upTo;
                }

            } while (true);
        }
    }
}