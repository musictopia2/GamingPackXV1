using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public interface ISavedCardList<D> where D : class, IDeckObject, new()
    {
        DeckRegularDict<D>? CardList { get; set; }
        D? CurrentCard { get; set; }
    }
}