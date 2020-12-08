namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public class ExcludeRCard //no interface for this.
    {
        public EnumSuitList Suit { get; set; }
        public int Number { get; set; } //i think that number is no problem.
        public ExcludeRCard(EnumSuitList suit, int number)
        {
            Suit = suit;
            Number = number;
        }
    }
}