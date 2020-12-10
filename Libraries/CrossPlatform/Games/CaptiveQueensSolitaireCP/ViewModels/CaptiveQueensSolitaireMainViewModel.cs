using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CaptiveQueensSolitaireCP.Data;
using CaptiveQueensSolitaireCP.Logic;
using CommonBasicStandardLibraries.Messenging;
namespace CaptiveQueensSolitaireCP.ViewModels
{
    [InstanceGame]
    public class CaptiveQueensSolitaireMainViewModel : SolitaireMainViewModel<CaptiveQueensSolitaireSaveInfo>
    {
        public CaptiveQueensSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
        }
        protected override SolitaireGameClass<CaptiveQueensSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<CaptiveQueensSolitaireMainGameClass>();
        }
        public int FirstNumber
        {
            get
            {
                return 5;
            }
        }
        public int SecondNumber
        {
            get
            {
                return 6;
            }
        }
    }
}