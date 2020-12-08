using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace GamePackageSignalRClasses
{
    public class SimpleClientClass
    {
        public string NickName { get; set; } = "";
        private readonly ISignalRInfo _connectInfo;
        HubConnection? _hubConnection;
        private bool _isConnected;
        public event EventHandler<CustomEventHandler>? OnReceivedMessage;
        private readonly IProgress<CustomEventHandler> _thisProgress;
        public SimpleClientClass(ISignalRInfo connectInfo)
        {
            _connectInfo = connectInfo;
            _thisProgress = new Progress<CustomEventHandler>(items =>
            {
                OnReceivedMessage?.Invoke(this, items);
            });
        }

        //public bool CustomEnabled => _hubConnection!.State == HubConnectionState.Connected;

        //there is also reconnecting.
        //so if i need something for reconnecting, that can be done as well.
        //next, there is disconnected.
        //this can give me ideas on things as well.
        //not sure if i will use it but possible.

        public async Task<bool> ConnectToServerAsync()
        {
            if (_isConnected == true)
            {
                throw new BasicBlankException("Already connected.  Rethink");
            }
            bool isAzure = await _connectInfo.IsAzureAsync();
            int port = 0;
            if (isAzure == false)
            {
                port = await _connectInfo.GetPortAsync();
            }
            string ipAddress = await _connectInfo.GetIPAddressAsync();
            string endPoint = await _connectInfo.GetEndPointAsync(); //i do like the interface method for this.
            if (isAzure == false)
            {
                    _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{ipAddress}:{port}{endPoint}"
                )
                .WithAutomaticReconnect()
                .Build();
            }
            else
            {
                _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{ipAddress}{endPoint}")
                .WithAutomaticReconnect()
                .Build();
            }
            _hubConnection.On("Hosting", () =>
            {
                _thisProgress.Report(new CustomEventHandler(EnumNetworkCategory.Hosting));
            });
            _hubConnection.On<string>("ConnectionError", Items =>
            {
                _thisProgress.Report(new CustomEventHandler(EnumNetworkCategory.Error, Items));
            });
            _hubConnection.On<string>("HostName", Items =>
            {
                _thisProgress.Report(new CustomEventHandler(EnumNetworkCategory.Client, Items)); //this will mean the client will get the host name.
            });
            _hubConnection.On("NoHost", () =>
            {
                _thisProgress.Report(new CustomEventHandler(EnumNetworkCategory.None)); //this means nobody is hosting.
            });

            _hubConnection.On<string>("ReceiveMessage", Items =>
            {
                _thisProgress.Report(new CustomEventHandler(EnumNetworkCategory.Message, Items));
            });
            _hubConnection.On("Close", () =>
            {
                PrivateDisconnectAsync();
            });
            try
            {
                await _hubConnection.StartAsync();
                _isConnected = true;
                return true; //i think this simple.
            }
            catch
            {
                return false;
            }
        }
        private async void PrivateDisconnectAsync()
        {
            await _hubConnection!.StopAsync();
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
            _isConnected = false;
        }
        public async Task HostGameAsync()
        {
            if (NickName == "")
            {
                throw new BasicBlankException("You need to specify a nick name in order to host game");
            }
            await _hubConnection.InvokeAsync("HostingAsync", NickName); //hopefully it works.
        }
        public async Task ConnectToHostAsync()
        {
            if (NickName == "")
            {
                throw new BasicBlankException("You need to specify a nick name in order to connect to host");
            }
            await _hubConnection.InvokeAsync("ClientConnectingAsync", NickName); //i think
        }
        public async Task SendMessageAsync(NetworkMessage ThisMessage)
        {
            string TempMessage = await js.SerializeObjectAsync(ThisMessage);
            await _hubConnection.InvokeAsync("SendMessageAsync", TempMessage);
        }
        public async Task DisconnectAllAsync()
        {
            if (_isConnected == false)
            {
                return;
            }
            if (_hubConnection == null)
            {
                return;
            }
            await _hubConnection.InvokeAsync("CloseAllAsync");
            await Task.Delay(100); //to give it time to receive message
            PrivateDisconnectAsync();
        }
    }
}