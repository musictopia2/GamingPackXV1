using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    public interface IScatterList<D> : ISimpleList<D>, IDeckShuffler<D> where D : IDeckObject, new() { }
}