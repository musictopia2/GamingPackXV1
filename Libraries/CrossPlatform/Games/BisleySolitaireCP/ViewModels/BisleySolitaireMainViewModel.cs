using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using BisleySolitaireCP.Data;
using BisleySolitaireCP.Logic;
using CommonBasicStandardLibraries.Messenging;
namespace BisleySolitaireCP.ViewModels
{
    [InstanceGame]
    public class BisleySolitaireMainViewModel : SolitaireMainViewModel<BisleySolitaireSaveInfo>
    {
        public BisleySolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<BisleySolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<BisleySolitaireMainGameClass>();
        }
    }
}