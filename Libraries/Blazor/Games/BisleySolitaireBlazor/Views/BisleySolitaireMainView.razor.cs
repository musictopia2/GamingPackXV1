using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using BisleySolitaireCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BisleySolitaireBlazor.Views
{
    public partial class BisleySolitaireMainView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Score", nameof(BisleySolitaireMainViewModel.Score)); //if there are others, do here.
            base.OnInitialized();
        }
        private string AutoMoveName => nameof(BisleySolitaireMainViewModel.AutoMoveAsync);
        private BasicMultiplePilesCP<SolitaireCard> GetMainPiles()
        {
            MainPilesCP main = (MainPilesCP)DataContext!.MainPiles1;
            var output = main.Piles;
            return output;
        }
        private BasicMultiplePilesCP<SolitaireCard> GetWastePiles()
        {
            WastePilesCP waste = (WastePilesCP)DataContext!.WastePiles1;
            var output = waste.Discards;
            return output!;
        }
    }
}