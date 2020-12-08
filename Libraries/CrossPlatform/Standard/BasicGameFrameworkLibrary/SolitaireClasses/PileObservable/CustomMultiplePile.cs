using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
namespace BasicGameFrameworkLibrary.SolitaireClasses.PileObservable
{
    public class CustomMultiplePile : BasicMultiplePilesCP<SolitaireCard>
    {
        protected override bool CanAutoUnselect()
        {
            return false;
        }
        public CustomMultiplePile(CommandContainer command) : base(command) { }
    }
}