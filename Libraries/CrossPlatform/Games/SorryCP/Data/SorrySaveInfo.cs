using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace SorryCP.Data
{
    [SingletonGame]
    public class SorrySaveInfo : BasicSavedGameClass<SorryPlayerItem>, ISavedCardList<CardInfo>
    {
        public CustomBasicList<MoveInfo> MoveList { get; set; } = new CustomBasicList<MoveInfo>();
        public EnumColorChoice OurColor { get; set; }
        public int PreviousPiece { get; set; }
        public CustomBasicList<int> HighlightList { get; set; } = new CustomBasicList<int>();
        public int MovesMade { get; set; }
        public int SpacesLeft { get; set; }
        public int PreviousSplit { get; set; }
        private SorryVMData? _thisMod;
        internal void LoadMod(SorryVMData thisMod)
        {
            _thisMod = thisMod;
            if (DidDraw == false)
            {
                _thisMod.CardDetails = "";
            }
            _thisMod.Instructions = Instructions;
        }
        private bool _didDraw;
        public bool DidDraw
        {
            get { return _didDraw; }
            set
            {
                if (SetProperty(ref _didDraw, value))
                {
                    if (_thisMod != null && value == false)
                    {
                        _thisMod.CardDetails = "";
                    }
                }
            }
        }
        private string _instructions = "";

        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    if (_thisMod != null)
                    {
                        _thisMod.Instructions = value;
                    }
                }
            }
        }
        public CardInfo? CurrentCard { get; set; }
        public DeckRegularDict<CardInfo>? CardList { get; set; } = new DeckRegularDict<CardInfo>(); //i think.
    }
}