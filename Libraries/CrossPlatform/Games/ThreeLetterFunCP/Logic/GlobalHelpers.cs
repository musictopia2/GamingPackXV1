using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ThreeLetterFunCP.Data;
namespace ThreeLetterFunCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GlobalHelpers
    {
        public CustomBasicList<WordInfo>? SavedWords;
        public CustomBasicList<char>? SavedTiles { get; set; }
        public CustomBasicList<SavedCard>? SavedCardList { get; set; }
        internal CustomStopWatchCP? Stops { get; set; }
        private readonly BasicData _basicData;
        private readonly ThreeLetterFunVMData _model;
        private readonly CommandContainer _command;
        #region "Delegates to stop overflow problems"
        public Func<bool, Task>? SelfGiveUpAsync { get; set; } //keep this is fine.
        #endregion
        public GlobalHelpers(BasicData basicData, ThreeLetterFunVMData model, CommandContainer command)
        {
            _basicData = basicData;
            _model = model;
            _command = command;
            LoadItems();
            PopulateWords();
        }
        private void LoadItems()
        {
            if (_basicData.MultiPlayer)
            {
                Stops = new CustomStopWatchCP();
                Stops.TimeUp += Stops_TimeUp;
                Stops.MaxTime = 120000;
            }
            _model.TileBoard1 = new TileBoardObservable(_command);
        }
        public void PopulateWords()
        {
            Assembly thisAssembly = Assembly.GetAssembly(GetType());
            string cardText = thisAssembly.ResourcesAllTextFromFile("cardlist.json");
            string tileText = thisAssembly.ResourcesAllTextFromFile("tilelist.json");
            SavedCardList = JsonConvert.DeserializeObject<CustomBasicList<SavedCard>>(cardText);
            var firstList = JsonConvert.DeserializeObject<CustomBasicList<SavedTile>>(tileText);
            SavedTiles = PrivateGetTiles(firstList);
            SavedWords = GetSavedWords(); //this is the slow part.  once you have it, no problem.

        }
        private async void Stops_TimeUp()
        {
            if (SelfGiveUpAsync == null)
            {
                throw new BasicBlankException("Nobody is handling the self giving up.  Rethink");
            }
            await SelfGiveUpAsync.Invoke(false);
        }
        private CustomBasicList<WordInfo> GetSavedWords()
        {
            using SpellingLibrary.SpellingLogic spells = new SpellingLibrary.SpellingLogic();
            CustomBasicList<WordInfo> output = new CustomBasicList<WordInfo>();
            var firstList = spells.GetWords(null, 3);
            firstList.ForEach(firstWord =>
            {
                WordInfo newWord = new WordInfo();
                newWord.Word = firstWord.Word;
                newWord.IsEasy = firstWord.Difficulty == SpellingLibrary.EnumDifficulty.Easy;
                output.Add(newWord);
            });
            return output;
        }
        private CustomBasicList<char> PrivateGetTiles(CustomBasicList<SavedTile> thisList)
        {
            CustomBasicList<char> finList = new CustomBasicList<char>();
            thisList.ForEach(thisTile =>
            {
                int x;
                var loopTo = thisTile.HowMany;
                for (x = 1; x <= loopTo; x++)
                    finList.Add(thisTile.Letter.Single());
            });
            return finList;
        }
        public CustomBasicList<TileInformation> GetTiles()
        {
            var thisList = Enumerable.Range(1, 100).ToCustomBasicList();
            return thisList.Select(items => GetTile(items)).ToCustomBasicList();
        }
        public TileInformation GetTile(int deck)
        {
            if (SavedTiles!.Count == 0)
            {
                throw new BasicBlankException("No saved tiles");
            }
            var tempTile = SavedTiles[deck - 1];
            TileInformation output = new TileInformation();
            output.Deck = deck;
            output.Letter = tempTile;
            return output;
        }
        public void PauseContinueTimer()
        {
            if (_basicData.MultiPlayer == false)
            {
                return; //because multiplayer has no timer.
            }
            if (Stops!.IsRunning == true)
            {
                Stops.PauseTimer();
            }
            else
            {
                Stops.ContinueTimer();
            }
        }
    }
}