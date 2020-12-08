using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.Messenging;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public class PlayerRummyHand<D> : PlayerSingleHand<D>, IPlayerRummyHand<D>, IHandle<UpdateCountEventModel>
        where D : IDeckObject, new()
    {
        public DeckRegularDict<D> AdditionalCards { get; set; } = new DeckRegularDict<D>(); //taking a risk.  hopefully it pays off.
        public override int ObjectCount => MainHandList.Count + _tempCards; //hopefully this simple.
        private int _tempCards;
        public void Handle(UpdateCountEventModel message)
        {
            _tempCards = message.ObjectCount;
        }
    }
}