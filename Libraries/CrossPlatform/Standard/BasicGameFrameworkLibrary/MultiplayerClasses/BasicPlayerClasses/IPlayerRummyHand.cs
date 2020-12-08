using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.Messenging;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerRummyHand<D> : IPlayerSingleHand<D>, IHandle<UpdateCountEventModel>
        where D : IDeckObject, new()
    {
        DeckRegularDict<D> AdditionalCards { get; set; }
    }
}