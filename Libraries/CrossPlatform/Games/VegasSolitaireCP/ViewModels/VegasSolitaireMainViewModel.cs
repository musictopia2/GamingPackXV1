using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using VegasSolitaireCP.Data;
using VegasSolitaireCP.Logic;
namespace VegasSolitaireCP.ViewModels
{
    [InstanceGame]
    public class VegasSolitaireMainViewModel : SolitaireMainViewModel<VegasSolitaireSaveInfo>
    {
        private decimal _money = 500; //start out with 500.
        private readonly MoneyModel _money1;
        public decimal Money
        {
            get { return _money; }
            set
            {
                if (SetProperty(ref _money, value))
                {
                    //can decide what to do when property changes
                    _money1.Money = value;
                }
            }
        }
        public VegasSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            MoneyModel money
            )
            : base(aggregator, command, resolver)
        {
            _money1 = money;
            Money = money.Money;
        }
        protected override SolitaireGameClass<VegasSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            VegasSolitaireMainGameClass game;
            game = resolver.ReplaceObject<VegasSolitaireMainGameClass>();
            game.AddMoney = (() => Money += 5);
            game.ResetMoney = (() => Money -= 52);
            return game;
        }
    }
}