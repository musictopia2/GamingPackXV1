using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using CommonBasicStandardLibraries.Exceptions;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.Extensions
{
    public static class GameContainerExtensions
    {
        public static void RepaintBoard(this IBasicGameContainer gameContainer)
        {
            gameContainer.Aggregator!.RepaintBoard();
        }
        public static async Task UIEndTurnAsync(this IBasicGameContainer gameContainer)
        {
            if (gameContainer.BasicData!.MultiPlayer)
            {
                await gameContainer.Network!.SendEndTurnAsync();
            }
            if (gameContainer.EndTurnAsync == null)
            {
                throw new BasicBlankException("Nobody is handling game end turn.  Rethink");
            }
            await gameContainer.EndTurnAsync.Invoke();
        }
        public static bool CanSendMessage<P, S>(this BasicGameContainer<P, S> gameContainer)
            where P : class, IPlayerItem, new()
            where S : BasicSavedGameClass<P>, new()
        {
            return gameContainer.SingleInfo!.CanSendMessage(gameContainer.BasicData);
        }
    }
}