using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.NetworkingClasses.Misc;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.Bootstrappers
{

    //hint:
    //its possible that bootstrappers has to be somewhere else now.
    //the only way i will know is when i start using mobile bindings.


    public abstract class BasicGameBootstrapper<TViewModel> : IGameBootstrapper, IHandleAsync<SocketErrorEventModel>,
        IHandleAsync<DisconnectEventModel>
         where TViewModel : IMainGPXShellVM
    {
        private readonly IStartUp? _startInfo;
        private readonly EnumGamePackageMode _mode;
        public BasicGameBootstrapper(IStartUp starts, EnumGamePackageMode mode)
        {
            JsonSettingsGlobals.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None; //try this way.  because otherwise, does not work if not everybody is .net core unfortunately.
            JsonSettingsGlobals.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None; //try this as well.  otherwise, gets hosed with .net core and xamarin forms.
            _startInfo = starts;
            _mode = mode;
            InitalizeAsync(); //maybe its okay to be async void.  doing the other is causing too many problems with the game package.
        }
        bool _isInitialized;

        public async void InitalizeAsync()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            UIPlatform.ShowError = (message) =>
            {
                UIPlatform.ShowMessageAsync($"There was an error.  Error was {message}.  Check console for details");
                Console.WriteLine(message);
            };
            GlobalDelegates.RefreshSubscriptions = (a =>
            {
                a.Subscribe(this);
            });
            await StartRuntimeAsync();
        }
        protected BasicData? GameData;
        protected TestOptions? TestData; //this is very important to have too.

        /// <summary>
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected async Task StartRuntimeAsync()
        {
            if (_mode == EnumGamePackageMode.None)
            {
                Console.WriteLine("Closing out because must be debug or production.");
                return;
            }
            StartUp(); //no operating system now.  if that changes, rethink.
            SetPersonalSettings(); //i think
            OurContainer = new GamePackageDIContainer();
            cons = OurContainer;
            FirstRegister();
            await ConfigureAsync();
            if (_mode == EnumGamePackageMode.Debug)
            {
                await RegisterTestsAsync();
            }
            if (UseMultiplayerProcesses)
            {
                OurContainer.RegisterType<BasicMessageProcessing>(true);
                IRegisterNetworks tempnets = OurContainer.Resolve<IRegisterNetworks>();
                tempnets.RegisterMultiplayerClasses(OurContainer); //since i commented out, maybe its okay now.
            }
            await DisplayRootViewForAsync(); //has to do here now.
        }
        protected virtual Task RegisterTestsAsync() { return Task.CompletedTask; }
        protected abstract bool UseMultiplayerProcesses { get; }
        protected virtual void StartUp() { }
        private void FirstRegister()
        {
            OurContainer!.RegisterSingleton(_startInfo); //because something else is asking for it.
            EventAggregator thisEvent = new EventAggregator();
            thisEvent.Subscribe(this); //maybe this was an issue.
            OurContainer!.RegisterSingleton(thisEvent); //put to list so if anything else needs it, can get from the container.
            CommandContainer thisCommand = new CommandContainer();
            OurContainer.RegisterSingleton(thisCommand);
            OurContainer.RegisterType<NewGameViewModel>(false); //bad news is its not working anyways.
            MiscRegisterFirst();
            OurContainer.RegisterSingleton(OurContainer);
            OurContainer.RegisterSingleton<IAsyncDelayer, AsyncDelayer>(); //for testing, will use a mock version.
            thisEvent.Subscribe(this); //no tags.
            GameData = new BasicData();
            GameData.GamePackageMode = _mode;
            OurContainer.RegisterSingleton(GameData);
            TestData = new TestOptions();
            OurContainer.RegisterSingleton(TestData);
            _startInfo!.RegisterCustomClasses(OurContainer, UseMultiplayerProcesses, GameData); //for now this way.  could change later.
        }
        protected virtual void MiscRegisterFirst() { }

        /// <summary>
        /// if we need custom registrations but still need standard, then override but do the regular functions too.
        /// </summary>
        /// <returns></returns>
        protected abstract Task ConfigureAsync();
        protected GamePackageDIContainer? OurContainer;
        protected virtual void SetPersonalSettings() { }
        protected async Task DisplayRootViewForAsync()
        {
            OurContainer!.RegisterType<TViewModel>(true);
            object item = cons!.Resolve<TViewModel>()!;
            if (item is IBlazorScreen screen)
            {
                await screen.ActivateAsync();
            }
        }
        protected virtual bool NeedExtraLocations { get; } = true;
        async Task IHandleAsync<SocketErrorEventModel>.HandleAsync(SocketErrorEventModel message)
        {
            if (message.Category == EnumSocketCategory.Client)
            {
                await UIPlatform.ShowMessageAsync($"Client Socket Error. The message was {message.Message}");
            }
            else if (message.Category == EnumSocketCategory.Server)
            {
                await UIPlatform.ShowMessageAsync($"Server Socket Error. The message was {message.Message}");
            }
            else
            {
                UIPlatform.ShowError("No Category Found For Socket Error");
            }
        }
        async Task IHandleAsync<DisconnectEventModel>.HandleAsync(DisconnectEventModel message)
        {

            await UIPlatform.ShowMessageAsync("Disconnected.  May have to refresh which starts all over again");
        }
    }
}