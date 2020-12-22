using BasicBlazorLibrary.Helpers;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Extensions;
using BasicGamingUIBlazorLibrary.StartupClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGamingUIBlazorLibrary.LocalStorageClasses
{
    public class MultiplayerAutoResumeClass : IMultiplayerSaveState
    {
        private readonly IGameInfo _game;
        private readonly BasicData _data;
        private readonly TestOptions _test;
        private LimitedList<IMappable>? _list; //has to be initialized eventually.  needs lazy loading.
        private IMappable? _previousObject;
        private readonly string _singleName;
        private readonly string _multiName;
        private readonly IJSRuntime _js;


        public MultiplayerAutoResumeClass(IGameInfo game, BasicData data, TestOptions test)
        {
            _game = game;
            _data = data;
            _test = test;
            _singleName = $"{game.GameName} Single";
            _multiName = $"{game.GameName} Multiplayer";
            if (GlobalStartUp.JsRuntime == null)
            {
                throw new BasicBlankException("No jsruntime used");
            }
            _js = GlobalStartUp.JsRuntime;
        }
        //await _js.StorageRemoveItemAsync(_game.GameName); //start with easy stuff.
        async Task IMultiplayerSaveState.DeleteGameAsync()
        {
            if (CanChange() == false)
            {
                return;
            }
            if (_list != null)
            {
                _list = new LimitedList<IMappable>(); //i think
            }
            _previousObject = null;
            string name = GetCurrentName();
            if (_js.ContainsKey(name) == false)
            {
                return;
            }
            await _js.StorageRemoveItemAsync(name);
        }

        private string GetCurrentName()
        {
            string name;
            if (_data.MultiPlayer == false)
            {
                name = _singleName;
            }
            else
            {
                name = _multiName;
            }
            return name;
        }

        private bool CanChange()
        {
            if (_game.CanAutoSave == false || _test.SaveOption != EnumTestSaveCategory.Normal)
            {
                return false;
            }
            return true;
        }

        async Task<EnumRestoreCategory> IMultiplayerSaveState.MultiplayerRestoreCategoryAsync()
        {
            await Task.CompletedTask;
            if (_test.SaveOption == EnumTestSaveCategory.NoSave)
            {
                return EnumRestoreCategory.NoRestore;
            }
            bool rets = _js.ContainsKey(_multiName);
            if (rets == false)
            {
                return EnumRestoreCategory.NoRestore; //because there is no autoresume game.
            }
            if (_test.SaveOption == EnumTestSaveCategory.RestoreOnly)
            {
                return EnumRestoreCategory.MustRestore;
            }
            return EnumRestoreCategory.CanRestore;
        }

        async Task<string> IMultiplayerSaveState.SavedDataAsync<T>()
        {
            if (_game.CanAutoSave == false)
            {
                return "";
            }
            if (_test.SaveOption == EnumTestSaveCategory.NoSave)
            {
                return "";
            }

            string name = GetCurrentName();
            if (_js.ContainsKey(name) == false)
            {
                return "";
            }
            try
            {
                LimitedList<T> temps = await _js.StorageGetItemAsync<LimitedList<T>>(name);
                _list = new LimitedList<IMappable>();
                _list.PopulateSavedList(temps);
                if (_test.StatePosition == 0)
                {
                    _previousObject = _list.MostRecent;
                }
                else
                {
                    _previousObject = _list[_test.StatePosition];
                }
                return JsonConvert.SerializeObject(_previousObject);
            }
            catch (Exception)
            {
                return "";
            }
        }
        //Task SaveStateAsync<T>(T thisState)
        //    where T : IMappable, new();


        private async void CallSaveStateAsync<T>(T thisState)
            where T: IMappable, new()
        {
            await SaveStateAsync(thisState);
        }

        private async Task SaveStateAsync<T>(T thisState)
             where T : IMappable, new()
        {
            await Task.Delay(5);
            if (CanChange() == false)
            {
                return;
            }

            string name = GetCurrentName();


            bool repeat;

            if (_previousObject != null)
            {
                string oldstr = JsonConvert.SerializeObject(_previousObject);
                string newStr = JsonConvert.SerializeObject(thisState);
                repeat = oldstr == newStr;
                _previousObject = thisState.AutoMap<T>();
            }
            else
            {
                _previousObject = thisState.AutoMap<T>();
                repeat = false;
            }
            if (_list == null)
            {
                _list = new LimitedList<IMappable>();
            }


            if (repeat == false)
            {
                _list.Add(_previousObject);
                //await fs.SaveObjectAsync(pathUsed, _list); //hopefully okay.
                await _js.UpdateLocalStorageAsync(name, _list); //hopefully this actually works out well.
            }
        }


        Task IMultiplayerSaveState.SaveStateAsync<T>(T thisState)
        {
            //hardest part because needs to delete everything else except for certain keys.
            CallSaveStateAsync(thisState);
            return Task.CompletedTask; //try this way now.  because too slow otherwise.
        }

        async Task<EnumRestoreCategory> IMultiplayerSaveState.SinglePlayerRestoreCategoryAsync()
        {
            await Task.CompletedTask;
            if (_test.SaveOption == EnumTestSaveCategory.NoSave)
            {
                return EnumRestoreCategory.NoRestore;
            }
            bool rets = _js.ContainsKey(_singleName);
            if (rets == false)
            {
                return EnumRestoreCategory.NoRestore; //because there is no autoresume game.
            }
            if (_test.SaveOption == EnumTestSaveCategory.RestoreOnly)
            {
                return EnumRestoreCategory.MustRestore;
            }
            return EnumRestoreCategory.CanRestore;
        }

        async Task<string> IMultiplayerSaveState.TempMultiSavedAsync()
        {
            if (_game.CanAutoSave == false || _test.SaveOption == EnumTestSaveCategory.NoSave)
            {
                return "";
            }
            
            if (_js.ContainsKey(_multiName) == false)
            {
                return "";
            }


            try
            {
                LimitedList<object> temps = await _js.StorageGetItemAsync<LimitedList<object>>(_multiName);
                var item = temps.MostRecent;
                return JsonConvert.SerializeObject(item);
            }
            catch (Exception)
            {
                return ""; //so it it can't deserialize at this stage, just return empty.
            }
        }
    }
}
