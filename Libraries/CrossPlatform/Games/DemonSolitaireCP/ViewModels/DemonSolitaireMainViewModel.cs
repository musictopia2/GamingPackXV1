using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using DemonSolitaireCP.Data;
using DemonSolitaireCP.Logic;
namespace DemonSolitaireCP.ViewModels
{
    [InstanceGame]
    public class DemonSolitaireMainViewModel : SolitaireMainViewModel<DemonSolitaireSaveInfo>
    {
        public DemonSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
            GlobalClass.MainModel = this;
            Heel1 = new DeckObservablePile<SolitaireCard>(command);
            Heel1.SendEnableProcesses(this, () => false);
            Heel1.DeckStyle = EnumDeckPileStyle.AlwaysKnown;
        }
        private int _startingNumber;
        public int StartingNumber
        {
            get { return _startingNumber; }
            set
            {
                if (SetProperty(ref _startingNumber, value))
                {   
                }
            }
        }
        protected override void CommandExecutingChanged()
        {
            StartingNumber = MainPiles1!.StartNumber();

        }
        public DeckObservablePile<SolitaireCard> Heel1;
        protected override SolitaireGameClass<DemonSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<DemonSolitaireMainGameClass>();
        }
    }
}