using CommonBasicStandardLibraries.CollectionClasses;
namespace PokerCP.Data
{
    public static class GlobalClass
    {
        public static CustomBasicCollection<DisplayCard> PokerList { get; set; } = new CustomBasicCollection<DisplayCard>();
    }
}