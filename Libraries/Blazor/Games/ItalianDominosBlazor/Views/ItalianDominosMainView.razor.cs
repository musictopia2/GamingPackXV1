using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using ItalianDominosCP.Data;
using ItalianDominosCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace ItalianDominosBlazor.Views
{
    public partial class ItalianDominosMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ItalianDominosMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(ItalianDominosMainViewModel.Status))
               .AddLabel("Up To", nameof(ItalianDominosMainViewModel.UpTo))
               .AddLabel("Next #", nameof(ItalianDominosMainViewModel.NextNumber));
            _scores.Clear();
            _scores.AddColumn("Total Score", true, nameof(ItalianDominosPlayerItem.TotalScore))
                .AddColumn("Dominos Left", true, nameof(ItalianDominosPlayerItem.ObjectCount))
                .AddColumn("Drew Yet", true, nameof(ItalianDominosPlayerItem.DrewYet), category: EnumScoreSpecialCategory.TrueFalse); //does not do it based on column
            base.OnInitialized();
        }
        public SimpleDominoInfo GetDomino
        {
            get
            {
                SimpleDominoInfo output = new SimpleDominoInfo();
                output.IsUnknown = true;
                output.Deck = 1; //needed so the back can show up properly.
                return output;
            }
        }
        private string PlayMethod => nameof(ItalianDominosMainViewModel.PlayAsync);
    }
}