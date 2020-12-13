using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BladesOfSteelCP.CustomPiles;
using Newtonsoft.Json;
namespace BladesOfSteelCP.Data
{
    public class BladesOfSteelPlayerItem : PlayerSingleHand<RegularSimpleCard>
    {
        private RegularSimpleCard? _faceOff;
        public RegularSimpleCard? FaceOff
        {
            get { return _faceOff; }
            set
            {
                if (SetProperty(ref _faceOff, value))
                {
                    
                }
            }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    
                }
            }
        }
        public DeckRegularDict<RegularSimpleCard> AttackList { get; set; } = new DeckRegularDict<RegularSimpleCard>();
        public DeckRegularDict<RegularSimpleCard> DefenseList { get; set; } = new DeckRegularDict<RegularSimpleCard>();
        [JsonIgnore]
        public PlayerAttackCP? AttackPile;
        [JsonIgnore]
        public PlayerDefenseCP? DefensePile;
    }
}