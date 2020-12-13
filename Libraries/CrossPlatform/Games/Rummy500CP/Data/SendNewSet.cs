using CommonBasicStandardLibraries.CollectionClasses;
namespace Rummy500CP.Data
{
    public class SendNewSet
    {
        public CustomBasicList<int> DeckList { get; set; } = new CustomBasicList<int>();
        public EnumWhatSets SetType { get; set; }
        public bool UseSecond { get; set; }
    }
}