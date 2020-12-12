using BasicBlazorLibrary.Helpers;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGamingUIBlazorLibrary.StartupClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace BasicGamingUIBlazorLibrary.LocalStorageClasses
{
    public class SinglePlayerAutoResumeClass : ISaveSinglePlayerClass
    {
        private readonly IGameInfo _game;
        private readonly BasicData _data;
        private readonly IJSRuntime _js;
        public static int RecentOne { get; set; } = 0; //0 means will be most recent.  otherwise, will show a past one.  helpful in testing.
        private LimitedList<IMappable>? _list;
        private IMappable? _previousObject;
        public SinglePlayerAutoResumeClass(IGameInfo game, BasicData data)
        {
            if (GlobalStartUp.JsRuntime == null)
            {
                throw new BasicBlankException("No jsruntime used");
            }
            _js = GlobalStartUp.JsRuntime;
            _game = game;
            _data = data;
        }
       
        Task<bool> ISaveSinglePlayerClass.CanOpenSavedSinglePlayerGameAsync()
        {
            if (_game.CanAutoSave == false)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(_js.ContainsKey(_game.GameName)); //hopefully this simple for this method.
        }

        async Task ISaveSinglePlayerClass.DeleteSinglePlayerGameAsync()
        {
            if (_js.ContainsKey(_game.GameName) == false)
            {
                return;
            }
            await _js.StorageRemoveItemAsync(_game.GameName); //start with easy stuff.
        }

        async Task<T> ISaveSinglePlayerClass.RetrieveSinglePlayerGameAsync<T>()
        {
            LimitedList<T> temps = await _js.StorageGetItemAsync<LimitedList<T>>(_game.GameName);
            _list = new LimitedList<IMappable>();
            _list.PopulateSavedList(temps);
            if (_data.GamePackageMode == EnumGamePackageMode.Production || RecentOne == 0)
            {
                _previousObject = _list.MostRecent.AutoMap<T>();
                return (T)_list.MostRecent!; //take a risk here too.
            }
            //in production, can add to history though.
            _previousObject = _list[RecentOne].AutoMap<T>();
            return (T)_list[RecentOne]!;

            //try/catch works for multiplayer but not for single player games.  if i run into problems, then rethink.

            //hint:
            //if retrieving the autoresume, may have to remove all except for 2 keys.
            //this should experiment with this as well.

            //should not have single player games mixed with multiplayer anyways.
            //this means this will remove all except for this one.
            //here is an idea.  the savegame will do a removeall before saving.


        }

        async Task ISaveSinglePlayerClass.SaveSimpleSinglePlayerGameAsync<T>(T thisObject)
        {
            if (_game.CanAutoSave == false)
            {
                return;
            }
            if (thisObject == null)
            {
                throw new BasicBlankException("Cannot save null object.  Rethink");
            }
            bool repeat;
            if (_previousObject != null)
            {
                //bool rets = _previousObject.Equals(thisObject);
                string oldstr = JsonConvert.SerializeObject(_previousObject);
                string newStr = JsonConvert.SerializeObject(thisObject);
                repeat = oldstr == newStr;
                _previousObject = thisObject.AutoMap<T>();
            }
            else
            {
                _previousObject = thisObject.AutoMap<T>();
                repeat = false;
            }

            if (_list == null)
            {
                _list = new LimitedList<IMappable>();
            }


            if (repeat == false)
            {
                _list.Add(_previousObject); //hopefully can handle this version without clearing out (?)
                await _js.StorageSetItemAsync(_game.GameName, _list);
            }
        }
    }
}