using BasicGameFrameworkLibrary.CommonInterfaces;
using System;
namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    public interface IRummmyObject<S, C> : ISimpleValueObject<int>, IWildObject,
       IIgnoreObject, ISuitObject<S>, IColorObject<C>
       where S : Enum where C : Enum
    {
        int GetSecondNumber { get; } //you should not be able to change it.
    }
}