using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using MinesweeperCP.Data;
using MinesweeperCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace MinesweeperCP.Logic
{
    public static class Helpers
    {
        public static async Task MessageGameOverAsync(this MinesweeperMainGameClass game, string message)
        {
            ToastPlatform.ShowInfo(message);
            await Task.Delay(2000); //wait 2 seconds so you can see the previous screen.
            await game.SendGameOverAsync();
        }
        public static void PopulateMinesNeeded(this ILevelVM level)
        {
            if ((int)level.LevelChosen == (int)EnumLevel.Easy)
            {
                level.HowManyMinesNeeded = 10;
            }
            else if ((int)level.LevelChosen == (int)EnumLevel.Medium)
            {
                level.HowManyMinesNeeded = 20;
            }
            else
            {
                level.HowManyMinesNeeded = 30;
            }
        }
    }
}