using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BowlingDiceGameCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BowlingDiceGameBlazor.Views
{
    public partial class BowlingDiceGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            DataContext!.CommandContainer.AddAction(ShowChange);
            _labels.AddLabel("Turn", nameof(BowlingDiceGameMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(BowlingDiceGameMainViewModel.Status))
               .AddLabel("Frame", nameof(BowlingDiceGameMainViewModel.WhatFrame));
            base.OnInitialized();
        }
        private string RollName => nameof(BowlingDiceGameMainViewModel.RollAsync);
        private string ContinueName => nameof(BowlingDiceGameMainViewModel.ContinueTurnAsync);
        private string EndTurnName => nameof(BowlingDiceGameMainViewModel.EndTurnAsync);
    }
}