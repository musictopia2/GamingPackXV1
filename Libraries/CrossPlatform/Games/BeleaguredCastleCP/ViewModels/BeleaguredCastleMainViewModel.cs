using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using BeleaguredCastleCP.Data;
using BeleaguredCastleCP.Logic;
using CommonBasicStandardLibraries.Messenging;
namespace BeleaguredCastleCP.ViewModels
{
    [InstanceGame]
    public class BeleaguredCastleMainViewModel : SolitaireMainViewModel<BeleaguredCastleSaveInfo>
    {
        public BeleaguredCastleMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<BeleaguredCastleSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<BeleaguredCastleMainGameClass>();
        }
    }
}