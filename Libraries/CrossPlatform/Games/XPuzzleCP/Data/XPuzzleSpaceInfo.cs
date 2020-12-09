using BasicGameFrameworkLibrary.GameBoardCollections;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;

namespace XPuzzleCP.Data
{
    public class XPuzzleSpaceInfo : ObservableObject, IBasicSpace
    {
        public Vector Vector { get; set; }

        private string _text = "";
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (SetProperty(ref _text, value) == true)
                {
                }
            }
        }

        private string _color = cs.Transparent; //now we use string.
        public string Color
        {
            get
            {
                return _color;
            }

            set
            {
                if (SetProperty(ref _color, value) == true)
                {
                }
            }
        }

        public void ClearSpace()
        {
            Color = cs.Transparent;
            Text = "";
        }

        public bool IsFilled()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }
    }
}