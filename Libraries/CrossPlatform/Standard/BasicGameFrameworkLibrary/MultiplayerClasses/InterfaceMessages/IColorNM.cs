using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages
{
    public interface IColorNM
    {
        Task ColorSentAsync(string data);
    }
}
