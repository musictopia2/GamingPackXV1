using SolitaireBoardGameCP.Data;
using SolitaireBoardGameCP.Logic;
using SolitaireBoardGameCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace SolitaireBoardGameBlazor.Views
{
    public partial class SolitaireBoardGameMainView
    {
        private SolitaireBoardGameCollection GetSpaceList()
        {
            SolitaireBoardGameSaveInfo thisSave = cons!.Resolve<SolitaireBoardGameSaveInfo>();


            return thisSave.SpaceList;
        }

        private string MethodName => nameof(SolitaireBoardGameMainViewModel.MakeMoveAsync);
    }
}