using BasicBlazorLibrary.Components.Basic;
using BasicBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.CollectionClasses;
using FroggiesCP.ViewModels;
namespace FroggiesBlazor.Views
{
    public partial class FroggiesMainView
    {
        private CustomBasicList<LabelGridModel> Labels { get; set; } = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            //i could no longer use the @key for the gameboard.  because otherwise, can't control whether it renders or not (?)
            Labels.Clear();
            Labels.AddLabel("Moves Left", nameof(FroggiesMainViewModel.MovesLeft))
                .AddLabel("How Many Frogs Currently", nameof(FroggiesMainViewModel.NumberOfFrogs))
                .AddLabel("How Many Frogs To Start", nameof(FroggiesMainViewModel.StartingFrogs));
            //if you have to add command change, do so as well.
            base.OnInitialized();
        }
        private static string MethodName => nameof(FroggiesMainViewModel.RedoAsync);

    }
}