using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using CommonBasicStandardLibraries.Exceptions;
using  cc = FluxxCP.Containers;
using FluxxCP.Data;
using System;
using System.Drawing;
namespace FluxxCP.Cards
{
    public class FluxxCardInformation : SimpleDeckObject, IDeckObject, IComparable<FluxxCardInformation>
    {
        public FluxxCardInformation()
        {
            DefaultSize = new SizeF(73, 113);
        }
        protected override void ChangeDeck()
        {
            Index = Deck;
        }
        public virtual void Populate(int chosen)
        {
            Deck = chosen;
        }
        public void Reset()
        {
            
        }
        public virtual string Text()
        {
            throw new BasicBlankException("Needs to override if text is needed");
        }
        public EnumCardType CardType { get; set; }
        public string Description { get; set; } = "";
        public virtual bool IncreaseOne() => false;
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
        protected void PopulateDescription()
        {
            Description = cc.FluxxGameContainer.DescriptionList[Deck - 1];
        }
        public bool CanDoCardAgain()
        {
            if (Deck == 1)
            {
                return false;
            }
            return CardType == EnumCardType.Action || CardType == EnumCardType.Rule;
        }
        int IComparable<FluxxCardInformation>.CompareTo(FluxxCardInformation? other)
        {
            return Deck.CompareTo(other!.Deck);
        }
        public override string GetKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}