using Microsoft.AspNetCore.Components;
using ThinkTwiceCP.Data;
using ThinkTwiceCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace ThinkTwiceBlazor.Views
{
    public partial class ScoreView
    {
        [CascadingParameter]
        public ScoreViewModel? DataContext { get; set; }
        private ThinkTwiceVMData? VMData { get; set; }

        protected override void OnInitialized()
        {
            VMData = cons!.Resolve<ThinkTwiceVMData>();
            base.OnInitialized();
        }
        private string GetButtonColor(string text)
        {
            if (VMData!.ItemSelected == -1)
            {
                return cc.Aqua;
            }
            string selected = VMData.TextList[VMData.ItemSelected];
            if (selected.Equals(text))
            {
                return cc.LimeGreen;
            }
            return cc.Aqua;
        }
        private string ChangeMethod => nameof(ScoreViewModel.ChangeSelection);
        private string ScoreMethod => nameof(ScoreViewModel.CalculateScoreAsync);
        private string DescriptionMethod => nameof(ScoreViewModel.ScoreDescriptionAsync);
    }
}