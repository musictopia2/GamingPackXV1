using CommonBasicStandardLibraries.Messenging;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface IAggregatorContainer
    {
        IEventAggregator Aggregator { get; } //sometimes, only is needed for a container and not just the ieventaggregator itself.
    }
}