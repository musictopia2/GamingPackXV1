using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.Extensions
{
    public static class BasicGameProcesses
    {
        public static async Task ProtectedGameOverNextAsync<P>(this IBasicGameProcesses<P> game) //no longer will send state since something else will delete it now.
            where P : class, IPlayerItem, new()
        {
            game.CurrentMod.NormalTurn = "None";
            if (game.BasicData!.MultiPlayer == true && game.BasicData.Client == true)
            {
                CommandContainer command = Resolve<CommandContainer>();
                command.ManuelFinish = true;
                command.IsExecuting = true; //to double check.  since its waiting for message.
                game.Check!.IsEnabled = true;
                return;
            }
            await game.SendGameOverAsync();
        }
        public static async Task RoundOverNextAsync<P>(this IBasicGameProcesses<P> game)
            where P : class, IPlayerItem, new()
        {
            CommandContainer command = Resolve<CommandContainer>();
            if (game.BasicData!.MultiPlayer == false || game.BasicData.Client == false)
            {
                game.CurrentMod.Status = "Goto the next round";
                await game.Aggregator.PublishAsync(new RoundOverEventModel());
            }
            else
            {
                
                game.CurrentMod.Status = "Waiting for host to goto the next round";
                game.Check!.IsEnabled = true;
                command.ManuelFinish = true;
                command.IsExecuting = true; //to double check.  since its waiting for message.
            }
            command.UpdateAll();
        }
        public static void ShowConnected<P>(this IBasicGameProcesses<P> game)
            where P : class, IPlayerItem, new()
        {
            game.CurrentMod.Status = "Connected";
        }
    }
}