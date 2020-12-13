using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using GalaxyCardGameCP.Cards;
using GalaxyCardGameCP.Logic;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace GalaxyCardGameCP.Data
{
    public class GalaxyCardGamePlayerItem : PlayerTrick<EnumSuitList, GalaxyCardGameCardInformation>
    {
        [JsonIgnore]
        public HandObservable<GalaxyCardGameCardInformation>? PlanetHand;
        public string PlanetData { get; set; } = "";
        [JsonIgnore]
        public MainSetsObservable<EnumSuitList, EnumRegularColorList, GalaxyCardGameCardInformation, MoonClass, SavedSet>? Moons;
        public CustomBasicList<SavedSet> SavedMoonList { get; set; } = new CustomBasicList<SavedSet>();
        private GalaxyCardGameMainGameClass? _mainGame;
        private GalaxyCardGameVMData? _model;
        public void LoadPiles(GalaxyCardGameMainGameClass mainGame, GalaxyCardGameVMData model, CommandContainer command)
        {
            _mainGame = mainGame;
            _model = model;
            PlanetHand = new HandObservable<GalaxyCardGameCardInformation>(command);
            PlanetHand.BoardClickedAsync += PlanetHand_BoardClickedAsync;
            PlanetHand.Maximum = 2;
            PlanetHand.Visible = true;
            PlanetHand.Text = $"{NickName} Planet";
            Moons = new MainSetsObservable<EnumSuitList, EnumRegularColorList, GalaxyCardGameCardInformation, MoonClass, SavedSet>(command);
            Moons.SetClickedAsync += Moons_SetClickedAsync;
            Moons.HasFrame = true;
            Moons.Text = $"{NickName} Moons";
        }
        public void ReportChange()
        {
            if (Moons == null || PlanetHand == null)
            {
                return;
            }
            Moons.ReportCanExecuteChange();
            PlanetHand.ReportCanExecuteChange();
        }
        public bool CanEnableMoon()
        {
            if (_mainGame!.SaveRoot!.GameStatus != EnumGameStatus.PlaceSets)
            {
                return false;
            }
            if (_mainGame.HasAutomaticPlanet())
            {
                return false;
            }
            return PlanetHand!.HandList.Count > 0;
        }
        private async Task Moons_SetClickedAsync(int setNumber, int section, int deck)
        {
            MoonClass thisMoon = Moons!.GetIndividualSet(setNumber);
            if (_mainGame!.CanAddToMoon(thisMoon, out GalaxyCardGameCardInformation? thisCard, out string message) == false)
            {
                ToastPlatform.ShowError($"{message} {Constants.vbCrLf}{Constants.vbCrLf}Because of the reason above, was unable to expand the moon");
                return;
            }
            if (_mainGame.BasicData!.MultiPlayer)
            {
                SendExpandedMoon temps = new SendExpandedMoon();
                temps.MoonID = setNumber;
                temps.Deck = thisCard!.Deck;
                await _mainGame.Network!.SendAllAsync("expandmoon", temps);
            }
            await _mainGame.AddToMoonAsync(thisCard!.Deck, setNumber);
        }
        private async Task PlanetHand_BoardClickedAsync()
        {
            var thisList = _model!.PlayerHand1!.ListSelectedObjects();
            if (thisList.Count > 2)
            {
                ToastPlatform.ShowError("Can only select 2 cards at the most for the planet");
                return;
            }
            if (_mainGame!.HasValidPlanet(thisList) == false)
            {
                ToastPlatform.ShowError("This is not a valid planet");
                return;
            }
            if (_mainGame.BasicData!.MultiPlayer)
            {
                var tempList = thisList.GetDeckListFromObjectList();
                await _mainGame.Network!.SendAllAsync("createplanet", tempList);
            }
            await _mainGame.CreatePlanetAsync(thisList);
        }
    }
}