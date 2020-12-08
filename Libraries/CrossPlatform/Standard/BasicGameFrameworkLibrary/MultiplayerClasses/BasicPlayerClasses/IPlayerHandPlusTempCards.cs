using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerHandPlusTempCards<D> : IPlayerSingleHand<D>
        where D : IDeckObject, new()
    {
        DeckRegularDict<D> AdditionalObjects { get; set; }
    }
}