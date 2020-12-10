using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using GrandfathersClockCP.Data;
using GrandfathersClockCP.Logic;
namespace GrandfathersClockCP.ViewModels
{
    [InstanceGame]
    public class GrandfathersClockMainViewModel : SolitaireMainViewModel<GrandfathersClockSaveInfo>
    {
        public GrandfathersClockMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<GrandfathersClockSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<GrandfathersClockMainGameClass>();
        }
    }
}