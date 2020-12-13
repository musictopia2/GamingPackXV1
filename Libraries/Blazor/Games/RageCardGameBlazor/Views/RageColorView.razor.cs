using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.GamePieceModels;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using RageCardGameCP.Data;
using RageCardGameCP.ViewModels;
namespace RageCardGameBlazor.Views
{
    public partial class RageColorView
    {
        private CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnParametersSet()
        {
            _scores = ScoreModule.GetScores();
            _labels.Clear();
            _labels.AddLabel("Trump", nameof(RageColorViewModel.TrumpSuit))
                .AddLabel("Lead", nameof(RageColorViewModel.Lead));
            base.OnParametersSet();
        }
        private string GetColor(BasicPickerData<EnumColor> piece) => piece.EnumValue.ToColor(); //i think.
    }
}