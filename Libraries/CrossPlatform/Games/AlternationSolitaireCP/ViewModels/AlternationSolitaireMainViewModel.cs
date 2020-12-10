using AlternationSolitaireCP.Data;
using AlternationSolitaireCP.Logic;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
namespace AlternationSolitaireCP.ViewModels
{
    [InstanceGame]
    public class AlternationSolitaireMainViewModel : SolitaireMainViewModel<AlternationSolitaireSaveInfo>
    {
        public AlternationSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<AlternationSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<AlternationSolitaireMainGameClass>();
        }
    }
}