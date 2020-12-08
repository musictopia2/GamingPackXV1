using System;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface ISuitObject<E> where E : Enum
    {
        E GetSuit { get; }
    }
}