using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public class BasicSavedDominosClass<D, P> : BasicSavedGameClass<P>
        where D : IDominoInfo, new()
        where P : class, IPlayerSingleHand<D>, new()
    {
        public SavedScatteringPieces<D>? BoneYardData { get; set; }
    }
}