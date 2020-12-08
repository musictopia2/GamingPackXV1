using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    public interface IDeckShuffler<D> : IDeckLookUp<D>, IDeckCount where D : IDeckObject, new() //this requires working with new.
    {
        bool NeedsToRedo { get; set; }//decided to require this too.
        Task<DeckRegularDict<D>> GetListFromJsonAsync(string jsonData); //hopefully this will work.
        void ClearObjects();
        void OrderedObjects(); //sometimes you want the cards with no shuffling
        void ShuffleObjects(); //i like the idea of shuffling and not simply populate.
        void ReshuffleFirstObjects(IDeckDict<D> thisList, int startAt, int endAt);
        void PutCardOnTop(int deck);
    }
}