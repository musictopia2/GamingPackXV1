using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using PersianSolitaireCP.Data;
using PersianSolitaireCP.Logic;
namespace PersianSolitaireCP.ViewModels
{
    [InstanceGame]
    public class PersianSolitaireMainViewModel : SolitaireMainViewModel<PersianSolitaireSaveInfo>
    {

        private int _dealNumber;

        public int DealNumber
        {
            get { return _dealNumber; }
            set
            {
                if (SetProperty(ref _dealNumber, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
        private readonly WastePiles _tempWaste;
        private readonly ISolitaireData _thisData;
        public PersianSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            ISolitaireData thisData
            )
            : base(aggregator, command, resolver)
        {
            _tempWaste = (WastePiles)WastePiles1;
            _thisData = thisData;
        }
        public bool CanNewDeal => _thisData.Deals != DealNumber;
        [Command(EnumCommandCategory.Plain)]
        public void NewDeal()
        {
            _tempWaste.Redeal();
        }
        protected override SolitaireGameClass<PersianSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            return resolver.ReplaceObject<PersianSolitaireMainGameClass>();
        }
    }
}