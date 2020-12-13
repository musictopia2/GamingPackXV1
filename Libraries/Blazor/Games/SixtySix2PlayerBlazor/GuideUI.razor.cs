using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SixtySix2PlayerCP.Data;
namespace SixtySix2PlayerBlazor
{
    public partial class GuideUI
    {
        [Parameter]
        public SixtySix2PlayerVMData? GameData { get; set; }
        private CustomBasicList<ScoreValuePair> _scores = new CustomBasicList<ScoreValuePair>();
        protected override void OnInitialized()
        {
            _scores = GameData!.GetDescriptionList();
            base.OnInitialized();
        }
    }
}