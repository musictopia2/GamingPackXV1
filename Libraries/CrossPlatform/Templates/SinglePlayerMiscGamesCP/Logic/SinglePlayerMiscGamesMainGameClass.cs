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
using SinglePlayerMiscGamesCP.Data;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using CommonBasicStandardLibraries.Messenging;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.DIContainers;

namespace SinglePlayerMiscGamesCP.Logic
{
    [SingletonGame]
    public class SinglePlayerMiscGamesMainGameClass : IAggregatorContainer
    {
        private readonly ISaveSinglePlayerClass _thisState;
        internal SinglePlayerMiscGamesSaveInfo _saveRoot;
        public SinglePlayerMiscGamesMainGameClass(ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IGamePackageResolver container
            )
        {
            _thisState = thisState;
            Aggregator = aggregator;
            _saveRoot = container.ReplaceObject<SinglePlayerMiscGamesSaveInfo>(); //can't create new one.  because if doing that, then anything that needs it won't have it.
        }

        private bool _opened;
        internal bool _gameGoing;

        public IEventAggregator Aggregator { get; }

        public async Task NewGameAsync()
        {
            _gameGoing = true;
            if (_opened == false)
            {
                _opened = true;
                if (await _thisState.CanOpenSavedSinglePlayerGameAsync())
                {
                    await RestoreGameAsync();
                    return;
                }
            }
        }
        private async Task RestoreGameAsync()
        {
            _saveRoot = await _thisState.RetrieveSinglePlayerGameAsync<SinglePlayerMiscGamesSaveInfo>();

        }
        public async Task ShowWinAsync()
        {
            _gameGoing = false;
            await UIPlatform.ShowMessageAsync("You Win");
            await _thisState.DeleteSinglePlayerGameAsync();
            //send message to show win.
            await this.SendGameOverAsync();

        }
    }
}