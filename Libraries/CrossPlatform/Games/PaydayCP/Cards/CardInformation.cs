using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using PaydayCP.Data;
using System;
namespace PaydayCP.Cards
{
    public class CardInformation : SimpleDeckObject, IDeckObject
    {
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
        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                if (SetProperty(ref _index, value))
                {
                    
                }
            }
        }
        public virtual void Populate(int chosen) { }
        public virtual void Reset() { }
        public override string GetKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}