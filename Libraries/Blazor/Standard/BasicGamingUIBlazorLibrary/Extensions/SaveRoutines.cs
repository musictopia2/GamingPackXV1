using BasicBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGamingUIBlazorLibrary.Extensions
{
    public static class SaveRoutines
    {
        public static async Task UpdateLocalStorageAsync(this IJSRuntime js, string key, object value)
        {
            CustomBasicList<string> saveList = StartupClasses.GlobalStartUp.KeysToSave;
            CustomBasicList<string> keyList = await js.GetKeyListAsync();
            await keyList.ForEachAsync(async key =>
            {
                if (saveList.Contains(key) == false)
                {
                    await js.StorageRemoveItemAsync(key); //hopefully can just delete anything not on the list.  in that case, keep the ones
                }
            });
            await js.StorageSetItemAsync(key, value);
        }
        //don't want to have to update the basic library just for this purpose.  hopefully this will work (no guarantees though).
        //besides the purpose is for gaming anyways.

        private static async Task<CustomBasicList<string>> GetKeyListAsync(this IJSRuntime js)
        {
            //this should just return the list of all keys.
            var length = await js.GetLengthAsync();
            CustomBasicList<string> output = new();
            for (int i = 0; i < length; i++)
            {
                int j = i;
                output.Add(await js.KeyAsync(j)); //just to make sure it does not get hosed.

            }
            return output;
        }
        private static async Task<string> KeyAsync(this IJSRuntime js, int index)
        {
            string output = await js.InvokeAsync<string>("localStorage.key", index);
            return output;
        }

        private static async Task<int> GetLengthAsync(this IJSRuntime js)
        {

            int output = await js.InvokeAsync<int>("eval", "localStorage.length");
            return output;
        }
    }
}
