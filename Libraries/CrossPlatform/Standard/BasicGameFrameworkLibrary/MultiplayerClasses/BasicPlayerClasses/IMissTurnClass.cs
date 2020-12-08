using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IMissTurnClass<P> where P : IPlayerItem
    {
        Task PlayerMissTurnAsync(P player);
    }
}