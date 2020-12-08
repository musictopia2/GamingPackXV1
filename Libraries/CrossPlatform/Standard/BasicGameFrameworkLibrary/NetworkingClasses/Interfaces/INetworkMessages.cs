using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Interfaces
{
    /// <summary>
    /// this interface is used which mostly communicates with the game.
    /// this is responsible for the rest.
    /// </summary>
    public interface INetworkMessages
    {
        Task<bool> InitNetworkMessagesAsync(string nickName, bool client);
        /// <summary>
        /// This will connect to host.
        /// Since subscribe is being used, then something else will happen eventually.
        /// all this cares about is connecting to host period.
        /// whoever implements this is responsible for figuring out what happens next.
        /// </summary>
        /// <returns></returns>
        Task ConnectToHostAsync();
        Task CloseConnectionAsync();
        Task SendToParticularPlayerAsync(string message, string toWho);
        Task SendToParticularPlayerAsync(string status, object content, string toWho);
        Task SendToParticularPlayerAsync(string status, string content, string toWho); //has to be more specific now.
        Task SendAllAsync(string message);
        Task SendAllAsync(string status, object content);
        Task SendAllAsync(string status, string content);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisList">list of messages usually sets</param>
        /// <param name="finalPart">the message identifier</param>
        /// <returns></returns>
        Task SendSeveralSetsAsync(ICustomBasicList<string> thisList, string finalPart); //i think the finalpart is still needed
    }
}