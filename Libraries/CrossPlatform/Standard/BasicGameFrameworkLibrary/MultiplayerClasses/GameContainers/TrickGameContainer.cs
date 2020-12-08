using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers
{
    public class TrickGameContainer<D, P, SA, TS> : CardGameContainer<D, P, SA>
        where TS : struct, Enum
        where D : class, ITrickCard<TS>, new()
        where P : class, IPlayerTrick<TS, D>, new()
        where SA : BasicSavedTrickGamesClass<TS, D, P>, new()

    {
        public TrickGameContainer(BasicData basicData,
            TestOptions test,
            IGameInfo gameInfo,
            IAsyncDelayer delay,
            IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            IListShuffler<D> deckList,
            RandomGenerator random) : base(basicData, test, gameInfo, delay, aggregator, command, resolver, deckList, random) { }
        public int SelfPlayer => PlayerList.Single(x => x.PlayerCategory == EnumPlayerCategory.Self).Id;
        public D GetBrandNewCard(int deck)
        {
            D thisCard = DeckList!.GetSpecificItem(deck);
            return (D)thisCard.CloneCard(); //hopefully this works.
        }
        public D GetSpecificCardFromDeck(int deck)
        {
            return DeckList!.GetSpecificItem(deck);
        }
        public Func<Task>? CardClickedAsync { get; set; }
        public Func<Task>? ContinueTrickAsync { get; set; }
        public Func<Task>? EndTrickAsync { get; set; }
    }
}