using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CaptiveQueensSolitaireCP.Logic;
using CaptiveQueensSolitaireCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
namespace CaptiveQueensSolitaireBlazor.Views
{
    public partial class CaptiveQueensSolitaireMainView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Score", nameof(CaptiveQueensSolitaireMainViewModel.Score))
                .AddLabel("First Start Number", nameof(CaptiveQueensSolitaireMainViewModel.FirstNumber))
                .AddLabel("Second Start Number", nameof(CaptiveQueensSolitaireMainViewModel.SecondNumber)); base.OnInitialized();
        }
        private string AutoMoveName => nameof(CaptiveQueensSolitaireMainViewModel.AutoMoveAsync);
        private CustomMain GetMainPiles()
        {
            CustomMain main = (CustomMain)DataContext!.MainPiles1;
            return main;
        }
        private SolitairePilesCP GetWastePiles()
        {
            WastePilesCP waste = (WastePilesCP)DataContext!.WastePiles1;
            var output = waste.Piles;
            return output;
        }
    }
}