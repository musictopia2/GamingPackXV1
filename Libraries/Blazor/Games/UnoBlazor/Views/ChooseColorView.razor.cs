using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.GamePieceModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using UnoCP.Data;
using UnoCP.ViewModels;
namespace UnoBlazor.Views
{
    public partial class ChooseColorView
    {
        [CascadingParameter]
        public ChooseColorViewModel? DataContext { get; set; }
        private UnoVMData? _data;
        protected override void OnParametersSet()
        {
            _data = DataContext!.Model;
            base.OnParametersSet();
        }
        private string GetColor(BasicPickerData<EnumColorTypes> piece) => piece.EnumValue.ToColor(); //i think.
    }
}