using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Interfaces
{
    public interface ITCPInfo
    {
        Task<string> GetIPAddressAsync();
        Task<int> GetPortAsync();
    }
}