using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data
{
    public class YahtzeePlayerItem<D> : SimplePlayer
        where D : SimpleDice, new()
    {
        private int _points;
        public int Points
        {
            get { return _points; }
            set
            {
                if (SetProperty(ref _points, value)) { }
            }
        }
        public CustomBasicList<RowInfo> RowList { get; set; } = new CustomBasicList<RowInfo>(); //this would be used for the scoresheets.
    }
}