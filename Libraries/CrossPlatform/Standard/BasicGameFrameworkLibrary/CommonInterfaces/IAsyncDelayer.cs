using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface IAsyncDelayer
    {
        Task DelaySeconds(double howLong);
        Task DelayMilli(int howLong); //this is needed because when using for unit testing, we don't want any delays.
    }
}