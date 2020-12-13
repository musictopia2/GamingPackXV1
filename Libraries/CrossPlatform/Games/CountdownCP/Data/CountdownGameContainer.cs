using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Threading.Tasks;
namespace CountdownCP.Data
{
    [SingletonGame]
    public class CountdownGameContainer : BasicGameContainer<CountdownPlayerItem, CountdownSaveInfo>
    {
        public CountdownGameContainer(BasicData basicData,
            TestOptions test,
            IGameInfo gameInfo,
            IAsyncDelayer delay,
            IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            RandomGenerator random) : base(basicData, test, gameInfo, delay, aggregator, command, resolver, random)
        {
        }
        //i assume it can't be internal because blazor needs it now.
        public Func<SimpleNumber, Task>? MakeMoveAsync { get; set; }
        public Func<CustomBasicList<SimpleNumber>>? GetNumberList { get; set; }
        public CustomBasicList<CountdownPlayerItem> GetPlayerList()
        {
            //this will return a list starting with self.
            if (BasicData.MultiPlayer == false)
            {
                return SaveRoot.PlayerList.ToCustomBasicCollection();
            }
            return SaveRoot.PlayerList.GetAllPlayersStartingWithSelf();
        }
    }
}