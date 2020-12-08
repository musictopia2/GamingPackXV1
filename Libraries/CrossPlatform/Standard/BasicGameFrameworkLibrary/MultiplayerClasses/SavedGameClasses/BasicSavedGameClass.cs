using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Newtonsoft.Json;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    /// <summary>
    /// This is the basic data needed to save a game.
    /// has to now inherit from simplesave to support the id part to make sure the private save matches the ids.
    /// </summary>
    /// <typeparam name="P">This is the player data.</typeparam>
    public abstract class BasicSavedGameClass<P> : SimpleSave, IMappable where P : class, IPlayerItem, new()
    {
        public bool NewRound { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public PlayOrderClass PlayOrder = new PlayOrderClass();
        public PlayerCollection<P> PlayerList = new PlayerCollection<P>();
        public bool ImmediatelyStartTurn { get; set; }// if set to true, then will mean that will go to startnewturn instead of continueturn.
    }
}