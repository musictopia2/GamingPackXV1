using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.SolitaireClasses.ClockClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using GrandfathersClockCP.Logic;
using GrandfathersClockCP.ViewModels;
namespace GrandfathersClockBlazor.Views
{
    public partial class GrandfathersClockMainView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Score", nameof(GrandfathersClockMainViewModel.Score)); //if there are others, do here.
            base.OnInitialized();
        }
        private string AutoMoveName => nameof(GrandfathersClockMainViewModel.AutoMoveAsync);
        private ClockObservable GetMainPiles()
        {
            CustomMain main = (CustomMain)DataContext!.MainPiles1;
            return main;
        }
        private SolitairePilesCP GetWastePiles()
        {
            WastePilesCP waste = (WastePilesCP)DataContext!.WastePiles1;
            var output = waste.Piles;
            return output;
            //return (WastePilesCP)DataContext!.WastePiles1;
        }
    }
}