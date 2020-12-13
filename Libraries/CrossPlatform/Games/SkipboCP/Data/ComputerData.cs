namespace SkipboCP.Data
{
    public class ComputerData
    {
        public EnumCardType WhichType { get; set; }
        public int Pile { get; set; }
        public Cards.SkipboCardInformation? ThisCard { get; set; }
        public int Discard { get; set; }
    }
}