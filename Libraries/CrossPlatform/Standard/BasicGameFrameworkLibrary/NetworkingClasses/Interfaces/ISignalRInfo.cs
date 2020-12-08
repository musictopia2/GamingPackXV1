using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Interfaces
{
    public interface ISignalRInfo : ITCPInfo
    {
        Task<bool> IsAzureAsync();
        Task<string> GetEndPointAsync();
    }
}