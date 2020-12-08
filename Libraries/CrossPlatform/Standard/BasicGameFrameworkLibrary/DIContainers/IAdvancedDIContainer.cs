namespace BasicGameFrameworkLibrary.DIContainers
{
    public interface IAdvancedDIContainer
    {
        IGamePackageResolver? MainContainer { get; set; }
    }
}