using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using LifeCardGameCP.Cards;
using LifeCardGameCP.Logic;
using Newtonsoft.Json;
namespace LifeCardGameCP.Data
{
    public class LifeCardGamePlayerItem : PlayerSingleHand<LifeCardGameCardInformation>
    {
        private int _points;

        public int Points
        {
            get { return _points; }
            set
            {
                if (SetProperty(ref _points, value))
                {
                    
                }
            }
        }
        public string LifeString { get; set; } = "";
        [JsonIgnore]
        public LifeStoryHand? LifeStory { get; set; }
    }
}