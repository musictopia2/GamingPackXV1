using System;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface IPopulateObject<T> where T : IConvertible
    {
        void Populate(T chosen); //so more options.
    }
}