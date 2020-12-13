using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.Data;
using LifeBoardGameCP.ViewModels;
namespace LifeBoardGameBlazor.Views
{
    public partial class ChooseGenderView
    {
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private string GetColor(EnumGender gender) => gender.ToColor();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ChooseGenderViewModel.Turn))
                .AddLabel("Instructions", nameof(ChooseGenderViewModel.Instructions));
            base.OnInitialized();
        }
    }
}