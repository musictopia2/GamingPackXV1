using MastermindCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace MastermindBlazor.Views
{
    public partial class MastermindMainView
    {
        //any code needed will go here.
        public string AcceptName => nameof(MastermindMainViewModel.AcceptAsync);
        public string GiveUpName => nameof(MastermindMainViewModel.GiveUpAsync);


        private string GetExtraMarginText()
        {
            string margins = "5px";
            return $"margin: {margins};";

        }
    }
}