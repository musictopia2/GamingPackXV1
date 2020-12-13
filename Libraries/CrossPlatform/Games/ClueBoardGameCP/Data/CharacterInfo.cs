namespace ClueBoardGameCP.Data
{
    public class CharacterInfo
    {
        public string Name { get; set; } = "";
        public int Player { get; set; }
        private EnumNameList _piece;
        public EnumNameList Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
                if (value == EnumNameList.Green)
                {
                    Name = "Mr. Green";
                }
                else if (value == EnumNameList.Mustard)
                {
                    Name = "Colonel Mustard";
                }
                else if (value == EnumNameList.Peacock)
                {
                    Name = "Mrs. Peacock";
                }
                else if (value == EnumNameList.Plum)
                {
                    Name = "Professor Plum";
                }
                else if (value == EnumNameList.Scarlet)
                {
                    Name = "Miss Scarlet";
                }
                else if (value == EnumNameList.White)
                {
                    Name = "Mrs. White";
                }
            }
        }
        public int CurrentRoom { get; set; }
        public int Space { get; set; }
        public int PreviousRoom { get; set; }
        public int FirstSpace { get; set; }
        public int FirstNumber { get; set; }
        public ComputerInfo ComputerData = new ComputerInfo();
        public string MainColor { get; set; } = "";
    }
}