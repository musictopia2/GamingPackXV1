using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CalculationSolitaireCP.Data;
using CalculationSolitaireCP.Logic;
using CommonBasicStandardLibraries.Messenging;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace CalculationSolitaireCP.ViewModels
{
    [InstanceGame]
    public class CalculationSolitaireMainViewModel : SolitaireMainViewModel<CalculationSolitaireSaveInfo>
    {
        public CalculationSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override Task ActivateAsync()
        {
            DeckPile!.DeckStyle = EnumDeckPileStyle.AlwaysKnown;
            return base.ActivateAsync();
        }
        protected override SolitaireGameClass<CalculationSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<CalculationSolitaireMainGameClass>();
        }
    }
}