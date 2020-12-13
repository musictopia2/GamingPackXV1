using CommonBasicStandardLibraries.CollectionClasses;
namespace FluxxCP.Data
{
    public class SavedActionClass
    {
        public int SelectedIndex { get; set; }
        public CustomBasicList<int> TempHandList { get; set; } = new CustomBasicList<int>();
        public CustomBasicList<PreviousCard> PreviousList { get; set; } = new CustomBasicList<PreviousCard>();
        public CustomBasicList<int> TempDiscardList { get; set; } = new CustomBasicList<int>();
    }
}