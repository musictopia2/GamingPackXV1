using BasicGameFrameworkLibrary.GameBoardCollections;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace ConnectFourCP.Data
{
    public class SpaceInfoCP : ObservableObject, IBasicSpace
    {
        public Vector Vector { get; set; }
        private int _player;
        public int Player
        {
            get { return _player; }
            set
            {
                if (SetProperty(ref _player, value))
                {
                    OnPropertyChanged(nameof(HasImage));
                }
            }
        }
        private string _color = cs.Transparent;
        public string Color
        {
            get { return _color; }
            set
            {
                if (SetProperty(ref _color, value))
                {
                    
                }
            }
        }
        public bool HasImage => Player > 0;
        public void ClearSpace()
        {
            Player = 0;
            Color = cs.Transparent;
        }
        public bool IsFilled()
        {
            return HasImage;
        }
    }
}