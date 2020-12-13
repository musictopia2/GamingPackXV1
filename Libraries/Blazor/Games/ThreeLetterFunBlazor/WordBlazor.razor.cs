using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ThreeLetterFunCP.Data;
using ThreeLetterFunCP.Logic;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace ThreeLetterFunBlazor
{
    public partial class WordBlazor
    {
        [Parameter]
        public ThreeLetterFunMainGameClass? MainGame { get; set; }
        [Parameter]
        public ThreeLetterFunCardData? DataContext { get; set; }
        private string GetFillColor()
        {
            if (MainGame!.SaveRoot!.Level == EnumLevel.Easy)
            {
                return cc.LimeGreen.ToWebColor();
            }
            return cc.DarkOrange.ToWebColor();
        }
        private readonly CustomBasicList<int> _lefts = new CustomBasicList<int>() { 3, 25, 47 };
        private async Task WordClickedAsync(EnumClickPosition position)
        {
            if (MainGame!.GameBoard.ObjectCommand.CanExecute(DataContext) == false)
            {
                return;
            }
            if (MainGame.SaveRoot.Level == EnumLevel.Easy)
            {
                DataContext!.ClickLocation = EnumClickPosition.Left; //always left for easy mode.
            }
            else
            {
                DataContext!.ClickLocation = position;
            }
            await MainGame.GameBoard.ObjectCommand.ExecuteAsync(DataContext);
        }
    }
}