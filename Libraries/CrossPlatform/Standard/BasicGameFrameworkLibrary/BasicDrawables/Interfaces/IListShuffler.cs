using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    public interface IListShuffler<D> : IDeckShuffler<D>, ISimpleList<D>, IEnumerableDeck<D> where D : IDeckObject, new()
    {
        void RelinkObject(int oldDeck, D newObject);
    }
}