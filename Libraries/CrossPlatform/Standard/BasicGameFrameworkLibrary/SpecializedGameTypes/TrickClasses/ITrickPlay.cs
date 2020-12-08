using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses
{
    public interface ITrickPlay
    {
        Task PlayCardAsync(int deck);
    }
}