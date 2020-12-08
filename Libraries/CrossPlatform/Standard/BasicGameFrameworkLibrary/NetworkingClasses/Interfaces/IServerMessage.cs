using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Interfaces
{
    public interface IServerMessage
    {
        Task ProcessDataAsync(SentMessage thisData);
    }
}