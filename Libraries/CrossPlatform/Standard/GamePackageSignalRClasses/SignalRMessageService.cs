using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings;
namespace GamePackageSignalRClasses
{
    public class SignalRMessageService : ObservableObject, IMessageChecker, INetworkMessages //not necessarily local anymore.
    {
        private readonly ISignalRInfo _thisTCP;
        private readonly IMessageProcessor _thisMessage;
        private readonly IGamePackageResolver _resolver;
        private readonly Queue<SentMessage> _messages = new Queue<SentMessage>();
        private readonly object _synLock = new object();
        private SimpleClientClass? _client1;
        private IOpeningMessenger? _thisOpen;
        public string NickName { get; set; } = "";
        public SignalRMessageService(ISignalRInfo thisTCP, IMessageProcessor thisMessage, IGamePackageResolver resolver)
        {
            _thisTCP = thisTCP;
            _thisMessage = thisMessage;
            _resolver = resolver;
        }
        public Task InitAsync()
        {
            _client1 = new SimpleClientClass(_thisTCP);
            _client1.OnReceivedMessage += Client1_OnReceivedMessage!;
            _client1.NickName = NickName;
            _thisOpen = _resolver.Resolve<IOpeningMessenger>();
            return Task.CompletedTask;
        }
        private async void Client1_OnReceivedMessage(object sender, CustomEventHandler e)
        {
            switch (e.Category)
            {
                case EnumNetworkCategory.None: //this means client did not find host
                    await _thisOpen!.HostNotFoundAsync(this);
                    break;
                case EnumNetworkCategory.Hosting:
                    IsEnabled = false;
                    await _thisOpen!.HostConnectedAsync(this);
                    break;
                case EnumNetworkCategory.Client:
                    IsEnabled = false;

                    await _thisOpen!.ConnectedToHostAsync(this, e.Message); //i think
                    break;
                case EnumNetworkCategory.CloseAll:
                    throw new BasicBlankException("I don't think we will close all here.  If I am wrong, rethink");
                case EnumNetworkCategory.Message:
                    SentMessage ThisData = await js.DeserializeObjectAsync<SentMessage>(e.Message);
                    await ProcessDataAsync(ThisData);
                    break;
                case EnumNetworkCategory.Error:
                    IsEnabled = false;
                    await _thisMessage.ProcessErrorAsync(e.Message);
                    break;
                default:
                    break;
            }
        }
        public async Task ProcessDataAsync(SentMessage thisData) //done.
        {
            if (IsEnabled == false)
            {
                lock (_synLock)
                {
                    _messages.Enqueue(thisData);
                }
                return;
            }
            IsEnabled = false;
            await _thisMessage.ProcessMessageAsync(thisData);
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
            _client1 = null!; //just get rid of it.  will just redo again.
        }
        public async Task SendAllAsync(string message)
        {
            SentMessage output = StartNewMessage(message);
            await SendAllAsync(output);
        }
        public async Task SendAllAsync(SentMessage tmessage)
        {
            NetworkMessage output = new NetworkMessage();
            output.Message = await js.SerializeObjectAsync(tmessage);
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
        public SentMessage StartNewMessage(string message)
        {
            SentMessage output = new SentMessage();
            output.Status = message;
            return output;
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
                {
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
                            }
                            _ = ProcessDataAsync(CurrentMessage);
                        }
                    }
                }
            }
        }
        public async Task SendToParticularPlayerAsync(string status, string data, string toWho)
        {
            SentMessage thisM = StartNewMessage(status, data); //nothing to deserialize.
            await SendToParticularPlayerAsync(thisM, toWho);
        }
        public async Task SendToParticularPlayerAsync(string status, object data, string toWho)
        {
            string news = await js.SerializeObjectAsync(data);
            SentMessage thisM = StartNewMessage(status, news);
            await SendToParticularPlayerAsync(thisM, toWho);
        }
        public async Task SendAllAsync(string status, object data)
        {
            string news = await js.SerializeObjectAsync(data);
            SentMessage thisM = StartNewMessage(status, news);
            await SendAllAsync(thisM);
        }
        public async Task SendAllAsync(string status, string data)
        {
            SentMessage thisM = StartNewMessage(status, data);
            await SendAllAsync(thisM);
        }
    }
}