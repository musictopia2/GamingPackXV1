using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using XPuzzleCP.Data;
namespace XPuzzleCP.Logic
{
    [SingletonGame]
    public class XPuzzleMainGameClass : IAggregatorContainer
    {
        private readonly ISaveSinglePlayerClass _thisState;
        internal XPuzzleSaveInfo _saveRoot;
        public XPuzzleMainGameClass(ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IGamePackageResolver container
            )
        {
            _thisState = thisState;
            Aggregator = aggregator;
            _saveRoot = container.ReplaceObject<XPuzzleSaveInfo>(); //can't create new one.  because if doing that, then anything that needs it won't have it.
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
            _saveRoot = await _thisState.RetrieveSinglePlayerGameAsync<XPuzzleSaveInfo>();
        }
        public async Task ShowWinAsync()
        {
            _gameGoing = false;
            await UIPlatform.ShowMessageAsync("You Win");
            await _thisState.DeleteSinglePlayerGameAsync();
            await this.SendGameOverAsync();
        }
    }
}