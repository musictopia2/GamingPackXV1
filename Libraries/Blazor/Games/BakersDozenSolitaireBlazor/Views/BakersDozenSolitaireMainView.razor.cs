using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using BakersDozenSolitaireCP.ViewModels;
namespace BakersDozenSolitaireBlazor.Views
{
    public partial class BakersDozenSolitaireMainView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Score", nameof(BakersDozenSolitaireMainViewModel.Score)); //if there are others, do here.
            base.OnInitialized();
        }
        private string AutoMoveName => nameof(BakersDozenSolitaireMainViewModel.AutoMoveAsync);
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