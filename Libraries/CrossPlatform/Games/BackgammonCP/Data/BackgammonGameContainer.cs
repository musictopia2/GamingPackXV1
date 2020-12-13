using BackgammonCP.Graphics;
using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BackgammonCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class BackgammonGameContainer : BasicGameContainer<BackgammonPlayerItem, BackgammonSaveInfo>
    {
        public BackgammonGameContainer(
            BasicData basicData,
            TestOptions test,
            IGameInfo gameInfo,
            IAsyncDelayer delay,
            IEventAggregator aggregator,
            CommandContainer command,
            IGamePackageResolver resolver,
            RandomGenerator random) : base(basicData,
                test,
                gameInfo,
                delay,
                aggregator,
                command,
                resolver,
                random)
        {
            Animates.LongestTravelTime = 100;
        }
        public bool RefreshPieces { get; set; }
        public Dictionary<int, TriangleClass> TriangleList { get; set; } = new Dictionary<int, TriangleClass>();
        public AnimateBasicGameBoard Animates { get; set; } = new AnimateBasicGameBoard();
        public bool MoveInProgress { get; set; }
        public CustomBasicList<MoveInfo> MoveList { get; set; } = new CustomBasicList<MoveInfo>();
        public int FirstDiceValue { get; set; }
        public int SecondDiceValue { get; set; }
        public bool HadDoubles()
        {
            if (FirstDiceValue == 0)
            {
                throw new BasicBlankException("The dice can never roll a 0.  Must populate the dice value first");
            }
            return FirstDiceValue == SecondDiceValue;
        }
        public Func<int, Task>? MakeMoveAsync { get; set; }
        public Action? DiceVisibleProcesses { get; set; }
    }
}