using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BladesOfSteelCP.Data;
using BladesOfSteelCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BladesOfSteelBlazor.Views
{
    public partial class FaceoffView
    {
        [CascadingParameter]
        public BladesOfSteelVMData? VMData { get; set; }
        [CascadingParameter]
        public FaceoffViewModel? DataContext { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Instructions", nameof(FaceoffViewModel.Instructions));
            base.OnInitialized();
        }
    }
}