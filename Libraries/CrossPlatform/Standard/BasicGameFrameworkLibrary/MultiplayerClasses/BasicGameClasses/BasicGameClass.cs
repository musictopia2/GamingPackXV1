using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses
{
    public abstract class BasicGameClass<P, S> :
        IGameSetUp<P, S>, IEndTurn, IEndTurnNM
        where P : class, IPlayerItem, new()
        where S : BasicSavedGameClass<P>, new()
    {

        public BasicGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            IViewModelData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            BasicGameContainer<P, S> gameContainer
            )
        {
            MainContainer = mainContainer;
            Aggregator = aggregator;
            BasicData = basicData;
            Test = test;
            _currentMod = currentMod;
            State = state;
            Delay = delay;
            _command = command;
            _gameContainer = gameContainer;
            SaveRoot = mainContainer.Resolve<S>(); //i think this would be fine (?)
            PlayerList = SaveRoot.PlayerList;
            gameContainer.EndTurnAsync = EndTurnAsync;
            gameContainer.ContinueTurnAsync = ContinueTurnAsync;
            gameContainer.ShowWinAsync = ShowWinAsync;
            gameContainer.StartNewTurnAsync = StartNewTurnAsync;
            gameContainer.SaveStateAsync = SaveStateAsync;
            Network = gameContainer.Network;
            Check = gameContainer.Check;
        }
        private bool _computerEndsTurn = false;
        bool IGameSetUp<P, S>.ComputerEndsTurn
        {
            set
            {
                _computerEndsTurn = value;
            }
        }
        private readonly IViewModelData _currentMod;
        IViewModelData IBasicGameProcesses<P>.CurrentMod => _currentMod;
        public IEventAggregator Aggregator { get; set; }
        public IGamePackageResolver MainContainer { get; set; }
        public IAsyncDelayer? Delay;
        private readonly CommandContainer _command;
        private readonly BasicGameContainer<P, S> _gameContainer;

        public S SaveRoot
        {
            get => _gameContainer.SaveRoot!;
            set => _gameContainer.SaveRoot = value;
        }
        protected bool IsLoaded; //so when loaded, then can act differently.
        public PlayerCollection<P> PlayerList { get; set; }
        protected IMultiplayerSaveState State; //not sure yet.
        //games like three letter fun needed to know the information about it.
        public BasicData BasicData { private set; get; } //this way others can access this data.
        public IMessageChecker? Check { get; set; }
        public INetworkMessages? Network { get; set; } //has to be completely open now.
        public TestOptions? Test { private set; get; }
        public P? SingleInfo
        {
            get => _gameContainer.SingleInfo;
            set => _gameContainer.SingleInfo = value;
        }

        public int WhoTurn
        {
            get => SaveRoot.PlayOrder.WhoTurn;
            set => SaveRoot.PlayOrder.WhoTurn = value;
        }
        protected int WhoStarts
        {
            get => SaveRoot.PlayOrder.WhoStarts;
            set => SaveRoot.PlayOrder.WhoStarts = value;
        }
        public virtual Task PopulateSaveRootAsync()
        {
            return Task.CompletedTask; //most of the time, nothing but can be something
        }
        public virtual bool CanMakeMainOptionsVisibleAtBeginning => true; //so others can decide not to after all.

        public Func<bool, Task>? FinishUpAsync { get; set; }

        /// <summary>
        /// this is after getting saved data.
        /// </summary>
        /// <returns></returns>
        public abstract Task FinishGetSavedAsync(); //changed my mind.
        public virtual Task EndTurnAsync() //this could change.
        {
            return Task.CompletedTask; //its up to each implementation to decide what to do.
        }
        /// <summary>
        /// you need to remember to have the loader finish up.  however, you can decide to call privatefinishasync as well.
        /// </summary>
        /// <param name="IsBeginning"></param>
        /// <returns></returns>
        public abstract Task SetUpGameAsync(bool isBeginning);
        protected async Task PrivateFinishAsync(bool isBeginning)
        {
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The finish up was not hooked up.  Rethink");
            }
            await FinishUpAsync.Invoke(isBeginning);
        }
        protected virtual Task ComputerTurnAsync()
        {
            return Task.CompletedTask; //if you do nothing, will be stuck.
        }
        protected virtual void GetPlayerToContinueTurn()
        {
            SingleInfo = PlayerList!.GetWhoPlayer();
        }
        protected virtual Task LoadPossibleOtherScreensAsync() { return Task.CompletedTask; } //most of the time nothing but we reserve the option to do something if necessary including async.
        protected virtual async Task ShowHumanCanPlayAsync() //reserve the right to be async.
        {
            _command.IsExecuting = false;
            _command.ManuelFinish = false; //does not have to manually be done anymore.
            _command.Processing = false; //just in case it was done another way.  hopefully i won't regret this.
            await Task.CompletedTask;
        }
        public async Task SaveStateAsync()
        {
            if (BasicData!.MultiPlayer == true && BasicData.Client == true)
            {
                return; //because clients can't save.  they don't even need the thisstate object
            }
            await PopulateSaveRootAsync();
            await State!.SaveStateAsync(SaveRoot!);
        }
        public virtual async Task ShowWinAsync() //because board games has to also do something else.
        {
            this.ProtectedShowWin();
            await this.ProtectedGameOverNextAsync();
        }
        protected async Task ShowWinAsync(string customMessage) //this is needed for games like millebournes.
        {
            this.ProtectedShowCustomWin(customMessage);
            await this.ProtectedGameOverNextAsync();
        }
        public virtual async Task ShowTieAsync() //because board games has to also do something else.
        {
            this.ProtectedShowTiec();
            await this.ProtectedGameOverNextAsync();
        }
        protected async Task ShowLossAsync() //for games like old maid.
        {
            this.ProtectedShowLoss();
            await this.ProtectedGameOverNextAsync();
        }
        public async virtual Task ContinueTurnAsync() //we do open the possibility of overriding if necessary.
        {
            await SaveStateAsync();
            GetPlayerToContinueTurn(); //usually will set to current turn but can be different.
            await LoadPossibleOtherScreensAsync();
            if (SingleInfo!.PlayerCategory == EnumPlayerCategory.Self)
            {
                await ShowHumanCanPlayAsync();
                _command.UpdateAll();
                return;
            }
            _command.ManuelFinish = true; //has to manually be done now.
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.Computer)
            {
                if (BasicData!.MultiPlayer == false || BasicData.Client == false)
                {
                    if (Test!.ComputerEndsTurn == true && BasicData.MultiPlayer == true)
                    {
                        throw new BasicBlankException("If the computer player is going to skip their turns and its multiplayer, then why did you add extra computer players?");
                    }
                    if (_computerEndsTurn) //needs to try to figure out another situation where the computer can't go even without testing.
                    {
                        _command.Processing = false; //because its testing anyways
                        if (Test.NoComputerPause == false)
                        {
                            await Delay!.DelayMilli(500); //i think you should see it showed computer player though.
                        }
                        await EndTurnAsync();
                        return; //forgot the return.
                    }
                    _command.UpdateAll();
                    await ComputerTurnAsync();
                    return;
                }
            }
            if (BasicData!.MultiPlayer == false)
            {
                throw new BasicBlankException("Stuck because not multiplayer.  Rethink");
            }
            Check!.IsEnabled = true; //to wait for messages again.
            _command.IsExecuting = true; //hopefully this is the way to go in order to double check.
            _command.UpdateAll(); //to show ui stuff.
        }
        public virtual async Task EndTurnReceivedAsync(string data)
        {
            await EndTurnAsync(); //most of the time, simple but you can override if necessary.
        }
        protected virtual void PrepStartTurn() //board games has to do something else in addition.  so upon autoresume, it can still if color is not chosen to show colors to choose.
        {
            SaveRoot!.ImmediatelyStartTurn = false;
            SingleInfo = PlayerList!.GetWhoPlayer();
            this.ShowTurn();
        }
        public abstract Task StartNewTurnAsync(); //decided to make it abstract this time.
    }
}