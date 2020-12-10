using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using FlorentineSolitaireCP.Data;
using FlorentineSolitaireCP.Logic;
namespace FlorentineSolitaireCP.ViewModels
{
    [InstanceGame]
    public class FlorentineSolitaireMainViewModel : SolitaireMainViewModel<FlorentineSolitaireSaveInfo>
    {
        public FlorentineSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<FlorentineSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<FlorentineSolitaireMainGameClass>();
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