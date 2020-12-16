using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Extensions
{
    public static class CommonMessages
    {
        public static async Task SendReadyMessageAsync(this INetworkMessages thisMessage, string yourName, string hostName)
        {
            await thisMessage.SendToParticularPlayerAsync("ready", yourName, hostName); //taking some risks here.
        }
        public static async Task SendLoadGameMessageAsync(this INetworkMessages thisMessage, object thisObject)
        {
            await thisMessage.SendAllAsync("loadgame", thisObject); //should have been loadgame, not newgame.
        }
        public static async Task SendNewGameAsync(this INetworkMessages thisMessage, object thisObject)
        {
            await thisMessage.SendAllAsync("newgame", thisObject);
        }
        public static async Task SendRestoreGameAsync(this INetworkMessages thisMessage, object thisObject)
        {
            await thisMessage.SendAllAsync("restoregame", thisObject); //try this way.  hopefully i don't need bypassing  if so, rethink again.
        }
        public static async Task SendMoveAsync(this INetworkMessages thisMessage, object thisObject)
        {
            await thisMessage.SendAllAsync("move", thisObject);
        }
        public static async Task SendDiscardMessageAsync(this INetworkMessages thisMessage, int deck)
        {
            await thisMessage.SendAllAsync("discard", deck);
        }
        public static async Task SendEndTurnAsync(this INetworkMessages thisMessage)
        {
            await thisMessage.SendAllAsync("endturn");
        }
        public static async Task SendDrawAsync(this INetworkMessages thisMessage)
        {
            await thisMessage.SendAllAsync("drawcard");
        }
        public static async Task SendPlayDominoAsync(this INetworkMessages thisMessage, int deck)
        {
            await thisMessage.SendAllAsync("playdomino", deck);
        }
        public static async Task SendCustomDeckListAsync<D>(this INetworkMessages message, string status, DeckRegularDict<D> list)
            where D: class, IDeckObject
        {
            var output = list.GetDeckListFromObjectList();
            await message.SendAllAsync(status, output);
        }
    }
}