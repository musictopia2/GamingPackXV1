using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using KlondikeSolitaireCP.Data;
using KlondikeSolitaireCP.Logic;
namespace KlondikeSolitaireCP.ViewModels
{
    [InstanceGame]
    public class KlondikeSolitaireMainViewModel : SolitaireMainViewModel<KlondikeSolitaireSaveInfo>
    {
        public KlondikeSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<KlondikeSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<KlondikeSolitaireMainGameClass>();
        }
    }
}