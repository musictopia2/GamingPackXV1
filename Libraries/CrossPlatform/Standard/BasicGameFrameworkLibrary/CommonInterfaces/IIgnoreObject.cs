namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface IIgnoreObject
    {
        bool IsObjectIgnored { get; } //so for games like phase 10, when figuring out the rummy stuff, it can not consider for those parts.
    }
}