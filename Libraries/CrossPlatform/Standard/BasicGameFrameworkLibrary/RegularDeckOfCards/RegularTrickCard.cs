using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using System.Drawing;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public class RegularTrickCard : RegularSimpleCard, ITrickCard<EnumSuitList>
    {
        public int Player { get; set; } //i don't think this needs binding.
        public virtual int GetPoints => Points; //different games can have different formulas for calculating points.
        public PointF Location { get; set; } //no bindings anymore for this.
        public object CloneCard()
        {
            return MemberwiseClone(); //hopefully this simple (?)
        }
    }
}