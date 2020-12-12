using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using BingoCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BingoCP.Data;
namespace BingoBlazor.Views
{
    public partial class BingoMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private string ColumnText => "50vw 50vw";
        private BingoSaveInfo? _save;
        protected override void OnInitialized()
        {
            _labels.Clear();
            DataContext!.CommandContainer.AddAction(ShowChange);
            _labels.AddLabel("Status", nameof(BingoMainViewModel.Status));
            //if you have to add command change, do so as well.
            base.OnInitialized();
        }
        private string SpaceName => nameof(BingoMainViewModel.SelectSpace);
        private string BingoName => nameof(BingoMainViewModel.BingoAsync);
        protected override void OnParametersSet()
        {
            _save = cons!.Resolve<BingoSaveInfo>();
            base.OnParametersSet();
        }
    }
}