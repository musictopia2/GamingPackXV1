using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using EagleWingsSolitaireCP.Data;
using EagleWingsSolitaireCP.Logic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace EagleWingsSolitaireCP.ViewModels
{
    [InstanceGame]
    public class EagleWingsSolitaireMainViewModel : SolitaireMainViewModel<EagleWingsSolitaireSaveInfo>
    {
        public DeckObservablePile<SolitaireCard> Heel1;
        public EagleWingsSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
            GlobalClass.MainModel = this;
            Heel1 = new DeckObservablePile<SolitaireCard>(command);
            Heel1.DeckClickedAsync += Heel1_DeckClickedAsync;
            Heel1.SendEnableProcesses(this, () => Heel1.CardsLeft() == 1);
        }
        private EagleWingsSolitaireMainGameClass? _mainGame;
        private async Task Heel1_DeckClickedAsync()
        {
            Heel1.IsSelected = true;
            await _mainGame!.HeelToMainAsync();
        }

        protected override SolitaireGameClass<EagleWingsSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            _mainGame = resolver.ReplaceObject<EagleWingsSolitaireMainGameClass>();
            return _mainGame;
        }
        private int _startingNumber;

        public int StartingNumber
        {
            get { return _startingNumber; }
            set
            {
                if (SetProperty(ref _startingNumber, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        protected override void CommandExecutingChanged()
        {
            StartingNumber = MainPiles1!.StartNumber();

        }
    }
}