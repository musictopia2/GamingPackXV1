using CommonBasicStandardLibraries.CollectionClasses;
namespace BackgammonCP.Data
{
    public class BackgammonPlayerDetails
    {
        public CustomBasicList<int>? PiecesOnBoard { get; set; }
        public int PiecesAtHome { get; set; }
        public int PiecesAtStart { get; set; }
    }
}