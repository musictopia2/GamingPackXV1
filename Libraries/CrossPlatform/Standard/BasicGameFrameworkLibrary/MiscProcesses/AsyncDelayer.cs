using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using static BasicGameFrameworkLibrary.DIContainers.Helpers;
namespace BasicGameFrameworkLibrary.MiscProcesses
{
    public class AsyncDelayer : IAsyncDelayer
    {
        async Task IAsyncDelayer.DelayMilli(int howLong)
        {
            await Task.Delay(howLong);
        }
        async Task IAsyncDelayer.DelaySeconds(double howLong)
        {
            await Task.Delay(TimeSpan.FromSeconds(howLong));
        }
        public static void SetDelayer(IAdvancedDIContainer thisMain, ref IAsyncDelayer delays)
        {
            PopulateContainer(thisMain);
            try
            {
                
                delays = thisMain.MainContainer!.Resolve<IAsyncDelayer>(""); //try this way now.  possible breaking change.
            }
            catch
            {
                delays = new AsyncDelayer(); //its okay to use default.
            }
        }
    }
}