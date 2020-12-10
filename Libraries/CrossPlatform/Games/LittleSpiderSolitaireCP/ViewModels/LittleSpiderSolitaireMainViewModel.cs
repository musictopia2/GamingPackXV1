using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using LittleSpiderSolitaireCP.Data;
using LittleSpiderSolitaireCP.Logic;
namespace LittleSpiderSolitaireCP.ViewModels
{
    [InstanceGame]
    public class LittleSpiderSolitaireMainViewModel : SolitaireMainViewModel<LittleSpiderSolitaireSaveInfo>
    {
        public LittleSpiderSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
            GlobalClass.MainModel = this;
        }
        protected override SolitaireGameClass<LittleSpiderSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<LittleSpiderSolitaireMainGameClass>();
        }
    }
}