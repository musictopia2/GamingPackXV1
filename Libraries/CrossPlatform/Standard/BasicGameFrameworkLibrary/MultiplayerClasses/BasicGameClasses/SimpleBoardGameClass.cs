using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses
{
    public abstract class SimpleBoardGameClass<P, S, E, M> : BasicGameClass<P, S>, IMoveProcesses<M>, IMoveNM, IAfterColorProcesses, IEraseColors
        where E : struct, Enum
         where P : class, IPlayerBoardGame<E>, new()

        where S : BasicSavedGameClass<P>, new()
    {
        private readonly ISimpleBoardGamesData _currentMod;

        public SimpleBoardGameClass(IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            ISimpleBoardGamesData currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            BasicGameContainer<P, S> gameContainer) : base(
                mainContainer,
                aggregator,
                basicData,
                test,
                currentMod,
                state,
                delay,
                command,
                gameContainer)
        {
            _currentMod = currentMod;
        }

        public abstract Task MakeMoveAsync(M space);
        protected override async Task ComputerTurnAsync()
        {
            if (PlayerList.DidChooseColors() == false)
            {
                if (MiscDelegates.ComputerChooseColorsAsync == null)
                {
                    throw new BasicBlankException("The computer choosing color was never handled.  Rethink");
                }
                await MiscDelegates.ComputerChooseColorsAsync.Invoke();
            }
        }
        public override Task ShowWinAsync()
        {
            _currentMod.Instructions = "None";
            return base.ShowWinAsync();
        }
        public override Task ShowTieAsync()
        {
            _currentMod.Instructions = "None";
            return base.ShowTieAsync();
        }

        public override bool CanMakeMainOptionsVisibleAtBeginning => PlayerList.DidChooseColors();

        protected bool CanPrepTurnOnSaved { get; set; } = true;
        protected void BoardGameSaved()
        {

            if (CanPrepTurnOnSaved)
            {
                PrepStartTurn();
            }
        }
        protected override async Task ShowHumanCanPlayAsync()
        {
            await base.ShowHumanCanPlayAsync();
            if (PlayerList.DidChooseColors() == false && MiscDelegates.ManuelSetColors != null)
            {
                MiscDelegates.ManuelSetColors.Invoke();
            }
        }
        protected override async Task LoadPossibleOtherScreensAsync()
        {
            if (PlayerList.DidChooseColors())
            {
                return; //because you already chose colors.
            }
            if (SingleInfo == null)
            {
                throw new BasicBlankException("Single info cannot be null when trying to load other possible screens.  Rethink");
            }
            if (MiscDelegates.ContinueColorsAsync == null)
            {
                return;
            }
            await MiscDelegates.ContinueColorsAsync.Invoke();
        }
        public void EraseColors()
        {
            PlayerList.EraseColors(); //should be this simple.  just for convenience.  maybe something else will do it (not sure).
        }
        async Task IMoveNM.MoveReceivedAsync(string data)
        {
            M item = await js.DeserializeObjectAsync<M>(data);
            await MakeMoveAsync(item);
        }
        /// <summary>
        /// by this time, something else already loaded the proper screens.
        /// this has to decide if anything else is needed like on game of life, loading yet another screen.
        /// </summary>
        /// <returns></returns>
        public abstract Task AfterChoosingColorsAsync();
    }
}