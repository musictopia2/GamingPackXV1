using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using PersianSolitaireCP.ViewModels;
namespace PersianSolitaireBlazor.Views
{
    public partial class PersianSolitaireMainView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Score", nameof(PersianSolitaireMainViewModel.Score))
                .AddLabel("Deal", nameof(PersianSolitaireMainViewModel.DealNumber))
                ; //if there are others, do here.
            base.OnInitialized();
        }
        private string AutoMoveName => nameof(PersianSolitaireMainViewModel.AutoMoveAsync);
        private string DealMethod => nameof(PersianSolitaireMainViewModel.NewDeal);
        private BasicMultiplePilesCP<SolitaireCard> GetMainPiles()
        {
            MainPilesCP main = (MainPilesCP)DataContext!.MainPiles1;
            var output = main.Piles;
            return output;
        }
        private SolitairePilesCP GetWastePiles()
        {
            WastePilesCP waste = (WastePilesCP)DataContext!.WastePiles1;
            var output = waste.Piles;
            return output;
        }
    }
}