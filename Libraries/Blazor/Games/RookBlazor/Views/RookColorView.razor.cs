using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.GamePieceModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using RookCP.ViewModels;
namespace RookBlazor.Views
{
    public partial class RookColorView
    {
        private string GetColor(BasicPickerData<EnumColorTypes> piece) => piece.EnumValue.ToColor();
        private string ChooseMethod => nameof(RookColorViewModel.TrumpAsync);
    }
}