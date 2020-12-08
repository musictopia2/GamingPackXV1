using System;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface ISimpleValueObject<V> where V : IConvertible //this would be good for cases where we need to calculate the straights as long as colors don't matter
    {
        V ReadMainValue { get; } //if somebody maps differently, then just make one equal to another.
    }
}