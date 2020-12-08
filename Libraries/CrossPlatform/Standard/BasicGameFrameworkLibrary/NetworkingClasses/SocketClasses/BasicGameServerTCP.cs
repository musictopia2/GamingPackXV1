using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.CollectionClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.NetworkingClasses.SocketClasses
{
    public class BasicGameServerTCP
    {
        private TcpListener? _mainListen;
        private readonly Dictionary<string, ClientInfo> _playerList = new Dictionary<string, ClientInfo>();
        private string _hostName = "";
        private readonly object _syncLock = new object();
        public void StartServer()
        {
            _mainListen = new TcpListener(IPAddress.Any, 8010); //decided to use 8010 this time.
            _mainListen.Start();
            AcceptMain();
        }
        private async void AcceptMain()
        {
            await Task.Run(() =>
            {
                do
                    try
                    {
                        var thisClient = _mainListen!.AcceptTcpClient();
                        ProcessMainClientRequests(thisClient);
                    }
                    catch (Exception)
                    {
                    }
                while (true);
            });
        }
        private void SendError(NetworkStream thisStream, string errorMessage)
        {
            var thisSend = new SentMessage();
            thisSend.Status = "Connection Error";
            thisSend.Body = errorMessage;
            var results = JsonConvert.SerializeObject(thisSend, Formatting.Indented);
            var ends = NetworkStreamHelpers.CreateDataPacket(results);
            thisStream.Write(ends, 0, ends.Length);
            thisStream.Flush(); //i think this too.
        }
        private async void ProcessMainClientRequests(TcpClient thisClient)
        {
            byte[]? ends = default;
            string errorMessage = "";
            ClientInfo? thisInfo = default;
            await Task.Run(() =>
            {
                do
                {
                    try
                    {
                        var thisStream = thisClient.GetStream();
                        var readInt = thisStream.ReadByte();
                        if (readInt == -1)
                        {
                            break;
                        }
                        if (readInt == 2)
                        {
                            var data = NetworkStreamHelpers.ReadStream(thisStream);
                            var thisStr = Encoding.ASCII.GetString(data);
                            NetworkMessage thisMessage = JsonConvert.DeserializeObject<NetworkMessage>(thisStr); //i think
                            errorMessage = ""; //has to be proven an error message.
                            switch (thisMessage.NetworkCategory)
                            {
                                case EnumNetworkCategory.None:
                                    break;
                                case EnumNetworkCategory.CloseAll:
                                    _hostName = "";
                                    lock (_syncLock)
                                    {
                                        foreach (var item in _playerList.Values)
                                        {
                                            item.ThisStream!.Flush();
                                            item.ThisStream.Close();
                                            item.ThisStream.Dispose();
                                            item.Socket!.Close();
                                            item.Socket.Dispose();
                                        }
                                        _playerList.Clear();
                                    }
                                    break;
                                case EnumNetworkCategory.Hosting:
                                    lock (_syncLock)
                                    {
                                        if (string.IsNullOrWhiteSpace(thisMessage.YourNickName))
                                        {
                                            SendError(thisStream, "You must enter a host name");
                                            Console.WriteLine("Hint.  Should have sent error");
                                            return;
                                        }
                                        _hostName = thisMessage.YourNickName; //i think the entire message will be nick name in this case.
                                        Console.WriteLine($"{_hostName} Is Hosting"); //to get hints.
                                        foreach (var item in _playerList.Values)
                                            item.ThisStream!.Dispose();
                                        _playerList.Clear();
                                        thisInfo = new ClientInfo();
                                        thisInfo.Socket = thisClient;
                                        thisInfo.ThisStream = thisStream;
                                        _playerList.Add(_hostName, thisInfo);
                                        SentMessage temp1 = new SentMessage();
                                        temp1.Status = "hosting"; //i think.
                                        string str1 = JsonConvert.SerializeObject(temp1, Formatting.Indented);
                                        ends = NetworkStreamHelpers.CreateDataPacket(str1);
                                        thisStream.Write(ends, 0, ends.Length); //confirmation.
                                        thisStream.Flush();
                                    }
                                    break;
                                case EnumNetworkCategory.Client:
                                    lock (_syncLock)
                                    {
                                        string TempNick = thisMessage.YourNickName;
                                        if (_playerList.ContainsKey(TempNick) == false)
                                        {
                                            thisInfo = new ClientInfo();
                                            thisInfo.Socket = thisClient;
                                            thisInfo.ThisStream = thisStream;
                                            _playerList.Add(TempNick, thisInfo);
                                        }
                                        else
                                        {
                                            thisInfo = _playerList[TempNick];
                                            thisInfo.ThisStream = thisStream;
                                            thisInfo.Socket = thisClient;
                                        }
                                        SentMessage temp2 = new SentMessage();
                                        temp2.Status = "clienthost";
                                        temp2.Body = _hostName;
                                        string str2 = JsonConvert.SerializeObject(temp2, Formatting.Indented);
                                        ends = NetworkStreamHelpers.CreateDataPacket(str2);
                                        thisStream.Write(ends, 0, ends.Length);
                                        thisStream.Flush(); //i think
                                    }
                                    break;
                                case EnumNetworkCategory.Message:
                                    lock (_syncLock)
                                    {
                                        if (thisMessage.SpecificPlayer != "")
                                        {
                                            if (_playerList.ContainsKey(thisMessage.SpecificPlayer) == false)
                                                errorMessage = $"{thisMessage.SpecificPlayer} was not even registered to receive messages";
                                            thisInfo = _playerList[thisMessage.SpecificPlayer];
                                            if (thisInfo.Socket!.Connected == false)
                                                errorMessage = $"{thisMessage.SpecificPlayer} was disconnected even though it showed it was registered";
                                            if (errorMessage != "")
                                            {
                                                SendError(thisStream, errorMessage);
                                                break;
                                            }
                                            ends = NetworkStreamHelpers.CreateDataPacket(thisMessage.Message);
                                            thisInfo.ThisStream!.Write(ends, 0, ends.Length);
                                            thisInfo.ThisStream.Flush(); //i think
                                            thisStream.Flush(); //i think here too.
                                            break;
                                        }
                                        //message gets sent to everybody except for self.
                                        CustomBasicList<ClientInfo> SendTo = _playerList.Where(Items => Items.Key != thisMessage.YourNickName).Select(Temps => Temps.Value).ToCustomBasicList();
                                        if (SendTo.Any(items => items.Socket!.Connected == false))
                                        {
                                            SendError(thisStream, "Somebody got disconnected when trying to send a message.");
                                            return;
                                        }
                                        SendTo.ForEach(items =>
                                        {
                                            ends = NetworkStreamHelpers.CreateDataPacket(thisMessage.Message);
                                            items.ThisStream!.Write(ends, 0, ends.Length);
                                            items.ThisStream.Flush(); //i think here too.
                                        });
                                        thisStream.Flush();
                                        break;
                                    }

                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        break; //not sure
                    }
                }
                    
                while (true);
            });
        }
    }
}