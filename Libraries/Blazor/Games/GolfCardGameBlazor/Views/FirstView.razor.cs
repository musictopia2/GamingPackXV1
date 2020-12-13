using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using GolfCardGameCP.Data;
using GolfCardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
namespace GolfCardGameBlazor.Views
{
    public partial class FirstView : IDisposable
    {
        [CascadingParameter]
        public GolfCardGameVMData? VMData { get; set; }
        [CascadingParameter]
        public FirstViewModel? DataContext { get; set; }
        private string ChooseMethod => nameof(FirstViewModel.ChooseFirstCardsAsync);
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Instructions", nameof(FirstViewModel.Instructions));
            DataContext!.CommandContainer.AddAction(ShowChange);
            base.OnInitialized();
        }
        private void ShowChange()
        {
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        void IDisposable.Dispose()
        {
            DataContext!.CommandContainer.RemoveAction(ShowChange);
        }
    }
}