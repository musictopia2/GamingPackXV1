using System.Collections.Generic;
namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    //needs to be this one to make it easy to track.
    //because it needs have something to do with this one since its cards but could be tiles, dominos, etc.
    public interface ISortObjects<D> : IComparer<D> where D : IDeckObject { }
}