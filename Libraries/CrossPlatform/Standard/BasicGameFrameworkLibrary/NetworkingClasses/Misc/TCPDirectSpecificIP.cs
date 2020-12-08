using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using BasicGameFrameworkLibrary.NetworkingClasses.SocketClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace BasicGameFrameworkLibrary.NetworkingClasses.Misc
{
    public class TCPDirectSpecificIP : ObservableObject, IMessageChecker, INetworkMessages, IServerMessage
    {
        private readonly ITCPInfo _thisTCP;
        private readonly IMessageProcessor _thisMessage;
        private readonly IGamePackageResolver _resolver;
        private readonly Queue<SentMessage> _messages = new Queue<SentMessage>();
        private readonly object _synLock = new object();
        private BasicGameClientTCP? _client1;
        public TCPDirectSpecificIP(ITCPInfo thisTCP, IMessageProcessor thisMessage, IGamePackageResolver resolver)
        {
            _thisTCP = thisTCP;
            _thisMessage = thisMessage;
            _resolver = resolver;
        }
        public Task InitAsync()
        {
            _client1 = new BasicGameClientTCP(this, _thisTCP);
            _client1.NickName = NickName;
            return Task.CompletedTask;
        }
        public async Task<bool> InitNetworkMessagesAsync(string nickName, bool client) //i think done for now for the sample.
        {
            bool rets;
            if (string.IsNullOrWhiteSpace(nickName))
            {
                throw new BasicBlankException("No Nick Name Upon Init Network Messages");
            }
            NickName = nickName;
            if (_client1 == null)
            {
                await InitAsync();
            }

            rets = await _client1!.ConnectToServerAsync(); //i think
            if (rets == false)
            {
                return false;
            }
            IsEnabled = true; //i think
            if (client == false)
            {
                await _client1.HostGameAsync();
            }
            return true;
        }
        public async Task ConnectToHostAsync()
        {
            await _client1!.ConnectToHostAsync();
        }
        public async Task CloseConnectionAsync()
        {
            await _client1!.DisconnectAllAsync();
            _messages.Clear(); //can clear all messages though.
            _client1 = null; //just get rid of it.  will just redo again.
        }
        public async Task SendAllAsync(string message)
        {
            SentMessage output = StartNewMessage(message);
            await SendAllAsync(output);
        }
        public async Task SendAllAsync(SentMessage tMessage)
        {
            NetworkMessage output = new NetworkMessage();
            output.Message = await js.SerializeObjectAsync(tMessage);
            output.YourNickName = NickName;
            await _client1!.SendMessageAsync(output);
        }
        public async Task SendSeveralSetsAsync(ICustomBasicList<string> thisList, string finalPart) //done
        {
            SentMessage output = StartNewMessage(finalPart);
            output.Body = await js.SerializeObjectAsync(thisList);
            await SendAllAsync(output);
        }
        public async Task SendToParticularPlayerAsync(string message, string toWho) //done.
        {
            SentMessage output = StartNewMessage(message);
            await SendToParticularPlayerAsync(output, toWho);
        }
        public async Task SendToParticularPlayerAsync(SentMessage message, string toWho)
        {
            NetworkMessage output = new NetworkMessage();
            output.SpecificPlayer = toWho;
            output.YourNickName = NickName;
            output.Message = await js.SerializeObjectAsync(message);
            await _client1!.SendMessageAsync(output);
        }
        public SentMessage StartNewMessage(string message) //done.
        {
            SentMessage output = new SentMessage();
            output.Status = message;
            return output; //i think
        }
        public SentMessage StartNewMessage(string message, string body)
        {
            SentMessage output = StartNewMessage(message);
            output.Body = body;
            return output;
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (SetProperty(ref _isEnabled, value))
                    //can decide what to do when property changes
                    if (value == true)
                    {
                        int Count;
                        lock (_synLock)
                        {
                            Count = _messages.Count;
                        }

                        if (Count > 0)
                        {

                            SentMessage CurrentMessage;
                            lock (_synLock)
                            {
                                CurrentMessage = _messages.Dequeue(); //try this.                                
                                _ = ProcessDataAsync(CurrentMessage);
                            }
                        }
                    }
            }
        }
        public string NickName { get; set; } = "";
        public async Task SendToParticularPlayerAsync(string status, object body, string toWho)
        {
            string News = await js.SerializeObjectAsync(body);
            SentMessage ThisM = StartNewMessage(status, News);
            await SendToParticularPlayerAsync(ThisM, toWho);
        }
        public async Task SendToParticularPlayerAsync(string status, string data, string toWho)
        {
            SentMessage thisM = StartNewMessage(status, data); //nothing to deserialize.
            await SendToParticularPlayerAsync(thisM, toWho);
        }
        public async Task SendAllAsync(string status, string data)
        {
            SentMessage thisM = StartNewMessage(status, data); //try this way.
            await SendAllAsync(thisM);
        }
        public async Task SendAllAsync(string Status, object Body)
        {
            string News = await js.SerializeObjectAsync(Body);
            SentMessage ThisM = StartNewMessage(Status, News);
            await SendAllAsync(ThisM);
        }
        public async Task ProcessDataAsync(SentMessage thisData) //done.
        {
            if (IsEnabled == false) //no more bypass.
            {
                lock (_synLock)
                    _messages.Enqueue(thisData);
                return;
            }
            IsEnabled = false;
            if (thisData.Status == "Connection Error")
            {
                await _thisMessage.ProcessErrorAsync(thisData.Body);
                return;
            }
            if (thisData.Status == "hosting")
            {
                var open = _resolver.Resolve<IOpeningMessenger>();
                await open.HostConnectedAsync(this);
                return;
            }
            if (thisData.Status == "clienthost")
            {
                var open = _resolver.Resolve<IOpeningMessenger>();
                if (thisData.Body != "")
                {
                    await open.ConnectedToHostAsync(this, thisData.Body);
                    return;
                }
                await open.HostNotFoundAsync(this);
                return;
            }
            await _thisMessage.ProcessMessageAsync(thisData);
        }
    }
}