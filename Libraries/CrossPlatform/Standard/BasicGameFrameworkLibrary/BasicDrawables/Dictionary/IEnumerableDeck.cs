using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using System.Collections.Generic;
namespace BasicGameFrameworkLibrary.BasicDrawables.Dictionary
{
    public interface IEnumerableDeck<D> : IEnumerable<D> where D : IDeckObject { }
}