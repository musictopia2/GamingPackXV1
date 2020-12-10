using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using EightOffSolitaireCP.Data;
using EightOffSolitaireCP.Logic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace EightOffSolitaireCP.ViewModels
{
    [InstanceGame]
    public class EightOffSolitaireMainViewModel : SolitaireMainViewModel<EightOffSolitaireSaveInfo>
    {
        [Command(EnumCommandCategory.Plain)]
        public async Task AddToReserveAsync()
        {
            await _mainGame!.AddToReserveAsync();
        }
        public ReservePiles ReservePiles1;
        private EightOffSolitaireMainGameClass? _mainGame;
        public EightOffSolitaireMainViewModel(IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver
            )
            : base(aggregator, command, resolver)
        {
            GlobalClass.MainModel = this;
            ReservePiles1 = new ReservePiles(command);
            ReservePiles1.Maximum = 8;
            ReservePiles1.AutoSelect = EnumHandAutoType.SelectOneOnly;
            ReservePiles1.Text = "Reserve Pile";
        }
        protected override SolitaireGameClass<EightOffSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
        {
            _mainGame = resolver.ReplaceObject<EightOffSolitaireMainGameClass>();
            return _mainGame;
        }
    }
}
