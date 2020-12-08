using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace MultiplayerSignalRHubClasses
{
    public class MultiplayerConnectionHub : Hub
    {
        private static readonly ConcurrentDictionary<string, ConnectionInfo> _playerList = new ConcurrentDictionary<string, ConnectionInfo>();
        private static string _hostName = "";
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionInfo thisCon = _playerList!.Where(x => x.Value.ConnectionID == Context.ConnectionId).Select(Items => Items.Value).SingleOrDefault()!;
            if (thisCon != null)
            {
                thisCon.IsConnected = false; //no longer connected
            }
            return base.OnDisconnectedAsync(exception);
        }
        private async Task SendErrorAsync(string errorMessage)
        {
            await Clients.Caller.SendAsync("ConnectionError", errorMessage);
        }
        public async Task HostingAsync(string nickName) //send message only to user.
        {
            _playerList.Clear();
            if (nickName == "")
            {
                await SendErrorAsync("You must have a nick name in order to host");
                return;
            }
            ConnectionInfo connect = new ConnectionInfo()
            {
                ConnectionID = Context.ConnectionId,
                UserID = nickName,
                IsConnected = true
            };
            _hostName = nickName;
            _playerList.TryAdd(nickName, connect);
            await Clients.Caller.SendAsync("Hosting"); //all you need to know is you are hosting now.
        }
        public async Task ClientConnectingAsync(string nickName)
        {
            if (_hostName == "")
            {
                await Clients.Caller.SendAsync("NoHost"); //because nobody is hosting.
                return;
            }
            if (_playerList.ContainsKey(nickName) == true)
            {
                await SendErrorAsync("You are already registered.  Rethink if intended this time");
                return;
            }
            ConnectionInfo connect = new ConnectionInfo()
            {
                ConnectionID = Context.ConnectionId,
                UserID = nickName,
                IsConnected = true
            };
            _playerList.TryAdd(nickName, connect);
            await Clients.Caller.SendAsync("HostName", _hostName);
        }
        public async Task CloseAllAsync()
        {
            _hostName = "";
            await Clients.Others.SendAsync("Close");
            _playerList.Clear(); //just act like nobody is connected anymore.
        }
        public async Task SendMessageAsync(string tempMessage)
        {
            string errorMessage = "";
            ConnectionInfo thisInfo;
            NetworkMessage thisMessage = await js.DeserializeObjectAsync<NetworkMessage>(tempMessage);
            if (_playerList.IsEmpty)
            {
                await SendErrorAsync("There are no players to send messages to");
                return;
            }
            if (thisMessage.SpecificPlayer != "")
            {
                if (_playerList.ContainsKey(thisMessage.SpecificPlayer) == false)
                {
                    errorMessage = $"{thisMessage.SpecificPlayer} was not even registered to receive messages";
                }
                thisInfo = _playerList[thisMessage.SpecificPlayer];
                if (thisInfo.IsConnected == false)
                {
                    errorMessage = $"{thisMessage.SpecificPlayer} was disconnected even though it showed it was registered";
                }
                if (errorMessage != "")
                {
                    await SendErrorAsync(errorMessage);
                    return;
                }
                await Clients.Client(thisInfo.ConnectionID).SendAsync("ReceiveMessage", thisMessage.Message);
                return; //i think
            }
            CustomBasicList<ConnectionInfo> SendTo = _playerList.Where(x => x.Key != thisMessage.YourNickName).Select(Temps => Temps.Value).ToCustomBasicList();
            if (SendTo.Any(Items => Items.IsConnected == false))
            {
                await SendErrorAsync("Somebody got disconnected when trying to send a message.");
                return;
            }
            await SendTo.ForEachAsync(async x =>
            {
                await Clients.Client(x.ConnectionID).SendAsync("ReceiveMessage", thisMessage.Message);
            });
        }
    }
}