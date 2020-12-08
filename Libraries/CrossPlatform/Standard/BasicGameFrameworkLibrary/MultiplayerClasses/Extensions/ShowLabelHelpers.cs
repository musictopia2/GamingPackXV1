using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.Extensions
{
    public static class ShowLabelHelpers
    {
        public static void ShowTurn<P>(this IBasicGameProcesses<P> game)
            where P : class, IPlayerItem, new()
        {
            if (game.CurrentMod == null)
            {
                return;
            }
            if (game.SingleInfo == null)
            {
                return;
            }
            game.SingleInfo = game.PlayerList!.GetWhoPlayer();
            game.CurrentMod.NormalTurn = game.SingleInfo.NickName;
        }
        public static void StartingStatus<P>(this IBasicGameProcesses<P> game)
            where P : class, IPlayerItem, new()
        {
            if (game.CurrentMod == null)
            {
                return;
            }
            if (game.BasicData!.MultiPlayer == true)
            {
                game.CurrentMod.Status = "Multiplayer game in progress";
            }
            else
            {
                game.CurrentMod.Status = "Single player game in progress";
            }
        }
        public static void ProtectedShowTiec<P>(this IBasicGameProcesses<P> game)
            where P : class, IPlayerItem, new()
        {
            game.CurrentMod.NormalTurn = "None";
            game.CurrentMod.Status = "Game Over.  It was a tie";
            ToastPlatform.ShowInfo("It was a tie");
        }
        public static void ProtectedShowWin<P>(this IBasicGameProcesses<P> game)
            where P : class, IPlayerItem, new()
        {
            game.CurrentMod.NormalTurn = "None";
            game.CurrentMod.Status = $"Game over.  {game.SingleInfo!.NickName} has won";
            CommandContainer command = Resolve<CommandContainer>();
            command.UpdateAll();
            if (game.BasicData!.MultiPlayer == false)
            {
                ToastPlatform.ShowInfo($"{game.SingleInfo.NickName} has won");
            }
            else if (game.SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
            {
                ToastPlatform.ShowSuccess($"{game.SingleInfo.NickName} wins the game");
            }
            else
            {
                ToastPlatform.ShowWarning("You lose the game");
            }
        }
        public static void ProtectedShowCustomWin<P>(this IBasicGameProcesses<P> game, string playersWonMessage)
            where P : class, IPlayerItem, new()
        {
            game.CurrentMod.NormalTurn = "None";
            game.CurrentMod.Status = $"Game Over.  {playersWonMessage} has won"; //this time, no messagebox.
        }
        public static void ProtectedShowLoss<P>(this IBasicGameProcesses<P> game) //this is for games like old maid.
            where P : class, IPlayerItem, new()
        {
            game.CurrentMod.NormalTurn = "None";
            game.CurrentMod.Status = $"Game over.  {game.SingleInfo!.NickName} has lost";
            ToastPlatform.ShowInfo($"{game.SingleInfo.NickName} is a loser");
        }
    }
}