using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerSingleHand<D> : IPlayerItem, IPlayerObject<D> where D : IDeckObject, new()
    {
        int ObjectCount { get; }
    }
}