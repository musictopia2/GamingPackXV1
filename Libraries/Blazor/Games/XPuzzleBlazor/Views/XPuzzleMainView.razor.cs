using BasicGameFrameworkLibrary.Extensions;
using System.Threading.Tasks;
using XPuzzleCP.Data;
using XPuzzleCP.Logic;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace XPuzzleBlazor.Views
{
    public partial class XPuzzleMainView
    {
        private XPuzzleCollection GetSpaceList()
        {
            XPuzzleSaveInfo thisSave = cons!.Resolve<XPuzzleSaveInfo>();
            return thisSave.SpaceList;
        }
        private async Task ClickedAsync(XPuzzleSpaceInfo space)
        {
            if (DataContext!.CommandContainer.CanExecuteBasics() == false)
            {
                return;
            }
            DataContext!.CommandContainer.StartExecuting(); //try this way.
            await DataContext.MakeMoveAsync(space); //this simple now.
            DataContext.CommandContainer.StopExecuting(); //try this way now.
        }
    }
}