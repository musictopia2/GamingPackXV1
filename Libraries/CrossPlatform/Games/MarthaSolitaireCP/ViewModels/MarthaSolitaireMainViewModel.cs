using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using MarthaSolitaireCP.Data;
using MarthaSolitaireCP.Logic;
namespace MarthaSolitaireCP.ViewModels
{
    [InstanceGame]
    public class MarthaSolitaireMainViewModel : SolitaireMainViewModel<MarthaSolitaireSaveInfo>
    {
        public MarthaSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<MarthaSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<MarthaSolitaireMainGameClass>();
        }
    }
}