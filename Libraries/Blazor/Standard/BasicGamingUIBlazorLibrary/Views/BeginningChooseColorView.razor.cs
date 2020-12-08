using BasicBlazorLibrary.Components.Basic;
using BasicBlazorLibrary.Helpers;
using BasicGameFrameworkLibrary.GamePieceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
namespace BasicGamingUIBlazorLibrary.Views
{
    public partial class BeginningChooseColorView<E, P> : IDisposable
        where E : struct, Enum
        where P : class, IPlayerBoardGame<E>, new()
    {
        [Parameter]
        public string TargetSize { get; set; } = "25vh"; //can be adjustable as needed.
        [Parameter]
        public RenderFragment<BasicPickerData<E>>? ChildContent { get; set; } //hopefully this simple
        [CascadingParameter]
        public BeginningChooseColorViewModel<E, P>? DataContext { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            DataContext!.CommandContainer.AddAction(ShowChange);
            _labels.AddLabel("Turn", nameof(IBeginningColorViewModel.Turn))
                .AddLabel("Instructions", nameof(IBeginningColorViewModel.Instructions));
            base.OnInitialized();
        }
        private void ShowChange()
        {
            InvokeAsync(() => StateHasChanged());
        }
        void IDisposable.Dispose()
        {
            DataContext!.CommandContainer.RemoveAction(ShowChange);
        }
    }
}