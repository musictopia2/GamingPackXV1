using System;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface IColorObject<E> where E : Enum //i think still needed.
    {
        E GetColor { get; }
    }
}