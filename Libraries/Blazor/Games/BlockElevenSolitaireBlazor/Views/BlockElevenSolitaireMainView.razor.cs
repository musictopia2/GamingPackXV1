using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using BlockElevenSolitaireCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BlockElevenSolitaireBlazor.Views
{
    public partial class BlockElevenSolitaireMainView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Score", nameof(BlockElevenSolitaireMainViewModel.Score))
                .AddLabel("Cards Left", nameof(BlockElevenSolitaireMainViewModel.CardsLeft));
            base.OnInitialized();
        }
        private BasicMultiplePilesCP<SolitaireCard> GetWastePiles()
        {
            WastePilesCP waste = (WastePilesCP)DataContext!.WastePiles1;
            var output = waste.Discards;
            return output!;
        }
    }
}