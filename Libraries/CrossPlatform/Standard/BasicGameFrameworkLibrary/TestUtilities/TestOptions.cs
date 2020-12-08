namespace BasicGameFrameworkLibrary.TestUtilities
{
    public class TestOptions
    {
        public bool ImmediatelyEndGame { get; set; } //if set to true, then a game can be over nearly right away.  used to easily test new game.
        public bool ComputerNoCards { get; set; } //i think this is a better option.
        public int CardsToPass { get; set; } //if something else is set, a player will get a different number.
        public bool DoubleCheck { get; set; } //if this is set to true, then some games would require double checking to begin with.
        public EnumTestSaveCategory SaveOption { get; set; }
        public int WhoStarts { get; set; } = 1;
        public EnumTestPlayCategory PlayCategory { get; set; } = EnumTestPlayCategory.Normal;
        public bool ComputerEndsTurn { get; set; } //if this is set, then the computer will always skip their turns.
        public bool NoAnimations { get; set; } //some games need it.
        public bool NoCommonMessages { get; set; }
        public bool NoComputerPause { get; set; } //if set to true, the computer will not even pause.
        public bool AllowAnyMove { get; set; } //in this case, any move is legal.
        public bool ShowErrorMessageBoxes { get; set; } = true; //if set to true, you will get a messagebox with the error message.
        public bool AutoNearEndOfDeckBeginning { get; set; } //i think its better that it will figure out how many to put to discard where you are near the end of the deck.
        public int StatePosition { get; set; } = 0; //0 means at beginning.
        public bool ShowNickNameOnShell { get; set; } //if set to true, then the main shell should show the nick name.  that is if i have 2 screens open and want to know who is who.
        public bool AlwaysNewGame { get; set; }
        public bool EndRoundEarly { get; set; } //this is used in cases where you have to end the round early to test new round.
    }
}