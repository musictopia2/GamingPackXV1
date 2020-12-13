using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.Messenging;
using SkuckCardGameCP.Cards;
using System;
using System.Threading.Tasks;
namespace SkuckCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class SkuckCardGameGameContainer : TrickGameContainer<SkuckCardGameCardInformation, SkuckCardGamePlayerItem, SkuckCardGameSaveInfo, EnumSuitList>
    {
        public SkuckCardGameGameContainer(BasicData basicData,
            TestOptions test,
            IGameInfo gameInfo,
            IAsyncDelayer delay,
            IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            IListShuffler<SkuckCardGameCardInformation> deckList,
            RandomGenerator random)
            : base(basicData, test, gameInfo, delay, aggregator, command, resolver, deckList, random)
        {
        }
        public Func<Task>? ComputerTurnAsync { get; set; }
        public Func<Task>? StartNewTrickAsync { get; set; }
        public Func<Task>? ShowHumanCanPlayAsync { get; set; }
    }
}