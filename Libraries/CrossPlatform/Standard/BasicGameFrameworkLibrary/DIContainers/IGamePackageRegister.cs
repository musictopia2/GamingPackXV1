using System;
namespace BasicGameFrameworkLibrary.DIContainers
{
    public interface IGamePackageRegister
    {
        void RegisterSingleton<TIn, TOut>() where TOut : TIn;
        void RegisterSingleton(Type thisType); //this means that you will register one type as singleton.
        void RegisterSingleton<TIn, TOut>(string tag);
        void RegisterSingleton(Type thisType, string tag);
        void RegisterTrue(string tag); //this means if somebody looks up the tag, it will be true;
        void RegisterInstanceType(Type type);
        /// <summary>
        /// what should happen is if the control is already registered, then replace.  otherwise, simply register.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="tag"></param>
        void RegisterControl(object control, string tag);
    }
}