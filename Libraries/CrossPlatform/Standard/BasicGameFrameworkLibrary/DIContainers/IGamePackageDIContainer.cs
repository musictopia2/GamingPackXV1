namespace BasicGameFrameworkLibrary.DIContainers
{
    public interface IGamePackageDIContainer : IGamePackageRegister
    {
        void DeleteRegistration<TIn>();
        bool LookUpValue(string tag);
        bool RegistrationExist<T>();
        bool RegistrationExist<T>(string tag);
        void RegisterSingleton<TIn>(TIn ourObject, string tag = "");
        void RegisterType<TIn>(bool isSingleton = true);
        void ReplaceObject<T>(T newObject);
        void ReplaceObject<T>(T newObject, string tag);
        void ReplaceRegistration<TIn, TOut>();
        T Resolve<T>();
        T Resolve<T>(string tag);
    }
}