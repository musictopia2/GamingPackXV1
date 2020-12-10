using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using RaglanSolitaireCP.Data;
using RaglanSolitaireCP.Logic;
namespace RaglanSolitaireCP.ViewModels
{
    [InstanceGame]
    public class RaglanSolitaireMainViewModel : SolitaireMainViewModel<RaglanSolitaireSaveInfo>
    {
        public RaglanSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
            Stock1 = new HandObservable<SolitaireCard>(command);
            Stock1.Maximum = 6;
            Stock1.Text = "Stock";
            GlobalClass.Stock = Stock1;
        }
        public HandObservable<SolitaireCard>? Stock1;
        protected override SolitaireGameClass<RaglanSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<RaglanSolitaireMainGameClass>();
        }
    }
}