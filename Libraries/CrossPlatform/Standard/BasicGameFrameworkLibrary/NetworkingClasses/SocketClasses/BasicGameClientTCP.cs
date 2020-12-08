using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace BasicGameFrameworkLibrary.NetworkingClasses.SocketClasses
{
    public class BasicGameClientTCP
    {
        private TcpClient? _client;
        private readonly IServerMessage? _processor;
        private readonly ITCPInfo? _connectInfo;
        public BasicGameClientTCP(IServerMessage processor, ITCPInfo connectInfo)
        {
            _processor = processor;
            _connectInfo = connectInfo;
        }
        public string NickName { get; set; } = "";
        public async Task<bool> ConnectToServerAsync()
        {
            int port = await _connectInfo!.GetPortAsync();
            string ipAddress = await _connectInfo.GetIPAddressAsync();
            await Task.Run(() =>
            {
                try
                {
                    _client = new TcpClient(ipAddress, port); //so this is background thread.
                }
                catch
                {

                }
            });
            if (_client == null)
                return false; //because you failed to connect to server.
            if (_client.Connected == true)
                ListenForMessagesAsync(); //no awaiting this time.
            return _client.Connected;
        }
        public bool IsConnected => _client!.Connected;
        private NetworkStream? _thisStream;
        private async void ListenForMessagesAsync()
        {
            _thisStream = _client!.GetStream();
            SentMessage? thisMessage = null;
            IProgress<int> thisProgress;
            thisProgress = new Progress<int>(async items =>
            {
                await _processor!.ProcessDataAsync(thisMessage!);
            });
            await Task.Run(() =>
            {
                do
                {
                    try
                    {
                        var readInt = _thisStream.ReadByte();
                        if (readInt == -1)
                            break;
                        if (readInt == 2)
                        {
                            var data = NetworkStreamHelpers.ReadStream(_thisStream);
                            var thisStr = Encoding.ASCII.GetString(data);
                            thisMessage = JsonConvert.DeserializeObject<SentMessage>(thisStr); //i think
                            thisProgress.Report(0);
                            _thisStream.Flush(); //i think
                        }
                    }
                    catch
                    {
                        break; //because its possible that you changed your mind.
                    }
                }
                while (true);
            });
        }
        public async Task SendMessageAsync(NetworkMessage thisMessage)
        {
            thisMessage.NetworkCategory = EnumNetworkCategory.Message; //i think
            await PrivateSendAsync(thisMessage);
        }
        public async Task HostGameAsync()
        {
            if (NickName == "")
            {
                throw new BasicBlankException("You need to specify a nick name in order to host game");
            }
            NetworkMessage thisMessage = new NetworkMessage();
            thisMessage.NetworkCategory = EnumNetworkCategory.Hosting;
            await PrivateSendAsync(thisMessage);
        }
        public async Task DisconnectAllAsync()
        {
            try
            {
                NetworkMessage thisMessage = new NetworkMessage();
                thisMessage.NetworkCategory = EnumNetworkCategory.CloseAll;
                await PrivateSendAsync(thisMessage);
            }
            finally
            {
                if (_client != null)
                {
                    _client.Close();
                    _client.Dispose();
                }
                if (_thisStream != null)
                {
                    _thisStream.Close();
                    _thisStream.Dispose();
                }
            }
        }
        private async Task PrivateSendAsync(NetworkMessage thisMessage)
        {
            if (string.IsNullOrWhiteSpace(NickName))
            {
                throw new BasicBlankException("Somehow nick name was never entered.  Rethink");
            }
            thisMessage.YourNickName = NickName;
            string results = await js.SerializeObjectAsync(thisMessage);
            var ends = NetworkStreamHelpers.CreateDataPacket(results);
            _thisStream!.Write(ends, 0, ends.Length);
            await _thisStream.FlushAsync(); //i think this too.
        }
        public async Task ConnectToHostAsync()
        {
            if (NickName == "")
            {
                throw new BasicBlankException("You need to specify a nick name in order to connect to host");
            }
            NetworkMessage thisMessage = new NetworkMessage();
            thisMessage.NetworkCategory = EnumNetworkCategory.Client;
            await PrivateSendAsync(thisMessage);
        }
    }
}