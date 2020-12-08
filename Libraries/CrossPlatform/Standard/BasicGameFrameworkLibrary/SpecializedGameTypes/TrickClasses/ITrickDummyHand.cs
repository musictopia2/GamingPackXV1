using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using System;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses
{
    public interface ITrickDummyHand<SU, TR> //only when possible dummy does this have to be used.
        where SU : Enum
        where TR : ITrickCard<SU>, new()
    {
        DeckRegularDict<TR> GetCurrentHandList(); //i think
        int CardSelected(); //in a case of dummy, has to figure out which card is actually selected.  otherwise, its from your hand.
        void RemoveCard(int deck); //this will only handle the removing of the card.
    }
}