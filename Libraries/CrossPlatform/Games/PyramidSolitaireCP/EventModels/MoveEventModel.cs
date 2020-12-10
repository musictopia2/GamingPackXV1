namespace PyramidSolitaireCP.EventModels
{
    public class MoveEventModel //not sure if we need this.  if so, will use (?)
    {
        public int Deck { get; set; }
        public MoveEventModel(int deck)
        {
            Deck = deck;
        }
    }
}