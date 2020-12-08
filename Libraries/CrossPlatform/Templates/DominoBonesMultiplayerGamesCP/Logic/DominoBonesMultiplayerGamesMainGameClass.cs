using System;
using System.Text;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Linq;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using fs = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.FileHelpers;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommonInterfaces;
using DominoBonesMultiplayerGamesCP.Data;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using CommonBasicStandardLibraries.Messenging;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Extensions; //most likely will be used.
using BasicGameFrameworkLibrary.Dominos;

namespace DominoBonesMultiplayerGamesCP.Logic
{
    [SingletonGame]
    public class DominoBonesMultiplayerGamesMainGameClass : DominosGameClass<SimpleDominoInfo, DominoBonesMultiplayerGamesPlayerItem, DominoBonesMultiplayerGamesSaveInfo>, IMiscDataNM
    {
        public DominoBonesMultiplayerGamesMainGameClass(IGamePackageResolver resolver,
            IEventAggregator aggregator,
            BasicData basic,
            TestOptions test,
            DominoBonesMultiplayerGamesVMData model,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            DominoBonesMultiplayerGamesGameContainer gameContainer
            ) : base(resolver, aggregator, basic, test, model, state, delay, command, gameContainer)
        {
            _model = model;
            DominosToPassOut = 6; //usually 6 but can be changed.

        }

        private readonly DominoBonesMultiplayerGamesVMData _model;

        public override Task FinishGetSavedAsync()
        {
            LoadControls();
            //anything else needed is here.
            return Task.CompletedTask;
        }
        private void LoadControls()
        {
            if (IsLoaded == true)
                return;

            IsLoaded = true; //i think needs to be here.
        }
        protected override async Task ComputerTurnAsync()
        {
            //if there is nothing, then just won't do anything.
            await Task.CompletedTask;
        }
        public override async Task SetUpGameAsync(bool isBeginning)
        {
            LoadControls();
            if (FinishUpAsync == null)
            {
                throw new BasicBlankException("The loader never set the finish up code.  Rethink");
            }
            await FinishUpAsync(isBeginning);
        }

        Task IMiscDataNM.MiscDataReceived(string status, string content)
        {
            switch (status) //can't do switch because we don't know what the cases are ahead of time.
            {
                //put in cases here.

                default:
                    throw new BasicBlankException($"Nothing for status {status}  with the message of {content}");
            }
        }
        public override async Task StartNewTurnAsync()
        {
            //maybe protectedstartturn (?)
            PrepStartTurn(); //anything else is below.

            await ContinueTurnAsync(); //most of the time, continue turn.  can change to what is needed
        }

        public override async Task PlayDominoAsync(int deck)
        {
            await SendPlayDominoAsync(deck); //if it can't send, won't.

            //TODO:  figure out how to play domino.
        }

        public override async Task EndTurnAsync() //usually the process for ending turn.
        {
            _model!.PlayerHand1!.EndTurn();
            WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
            await StartNewTurnAsync();
        }
        public override Task PopulateSaveRootAsync() //usually needs this too.
        {
            ProtectedSaveBone();
            return Task.CompletedTask;
        }

    }
}