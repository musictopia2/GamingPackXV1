using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using LifeBoardGameCP.Data;
using System;
using System.Drawing;
namespace LifeBoardGameCP.Cards
{
    public class LifeBaseCard : SimpleDeckObject, IDeckObject //can't be abstract or can't autosave cards.
    {
        public LifeBaseCard()
        {
            DefaultSize = new SizeF(80, 100);
        }
        private EnumCardCategory _cardCategory;
        public EnumCardCategory CardCategory
        {
            get { return _cardCategory; }
            set
            {
                if (SetProperty(ref _cardCategory, value))
                {
                    
                }
            }
        }
        public void Populate(int chosen) { }
        public void Reset() { }
        public override string GetKey()
        {
            return Guid.NewGuid().ToString(); //try this way even though performance hit.
        }
    }
}