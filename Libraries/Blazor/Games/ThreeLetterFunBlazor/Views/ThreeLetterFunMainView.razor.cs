using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using ThreeLetterFunCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
using ThreeLetterFunCP.Data;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using ThreeLetterFunCP.Logic;

namespace ThreeLetterFunBlazor.Views
{
    public partial class ThreeLetterFunMainView : INewCard
    {
        private ThreeLetterFunCardData? _tempCard;
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _scores.AddColumn("Cards Won", true, nameof(ThreeLetterFunPlayerItem.CardsWon));
            DataContext!.GameData.NewUI = this; //try this.
            base.OnInitialized();
        }
        private TileBoardBlazor? Board { get; set; }
        void INewCard.ShowNewCard()
        {
            _tempCard = DataContext!.CurrentCard; //hopefully this simple.
            ShowChange(); //try this way.
        }
        private string PlayMethod => nameof(ThreeLetterFunMainViewModel.PlayAsync);
        private string GiveUpMethod => nameof(ThreeLetterFunMainViewModel.GiveUpAsync);
        private string TakeBackMethod => nameof(ThreeLetterFunMainViewModel.TakeBack);
    }
}