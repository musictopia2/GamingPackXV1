using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses
{
    public interface ILoadClientGame
    {
        Task LoadGameAsync(string payLoad);
    }
}