using BakersDozenSolitaireCP.Data;
using BakersDozenSolitaireCP.Logic;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
namespace BakersDozenSolitaireCP.ViewModels
{
    [InstanceGame]
    public class BakersDozenSolitaireMainViewModel : SolitaireMainViewModel<BakersDozenSolitaireSaveInfo>
    {
        public BakersDozenSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<BakersDozenSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<BakersDozenSolitaireMainGameClass>();
        }
    }
}