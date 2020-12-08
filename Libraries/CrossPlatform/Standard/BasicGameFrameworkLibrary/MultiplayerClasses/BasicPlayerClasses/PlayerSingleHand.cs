using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using Newtonsoft.Json;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public class PlayerSingleHand<D> : SimplePlayer, IPlayerSingleHand<D> where D : IDeckObject, new()
    {


        public virtual int ObjectCount => MainHandList.Count;
        public DeckRegularDict<D> MainHandList { get; set; } = new DeckRegularDict<D>();
        [JsonIgnore]
        public DeckRegularDict<D> StartUpList { get; set; } = new DeckRegularDict<D>();
    }
}