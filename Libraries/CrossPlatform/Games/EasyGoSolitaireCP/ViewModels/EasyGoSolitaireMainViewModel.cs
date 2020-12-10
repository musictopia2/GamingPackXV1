using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using EasyGoSolitaireCP.Data;
using EasyGoSolitaireCP.Logic;
namespace EasyGoSolitaireCP.ViewModels
{
    [InstanceGame]
    public class EasyGoSolitaireMainViewModel : SolitaireMainViewModel<EasyGoSolitaireSaveInfo>
    {
        public EasyGoSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<EasyGoSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<EasyGoSolitaireMainGameClass>();
        }
    }
}