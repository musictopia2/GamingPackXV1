using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using LittleSpiderSolitaireCP.ViewModels;
namespace LittleSpiderSolitaireBlazor.Views
{
    public partial class LittleSpiderSolitaireMainView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Score", nameof(LittleSpiderSolitaireMainViewModel.Score)); //if there are others, do here.
            base.OnInitialized();
        }
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