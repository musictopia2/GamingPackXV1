using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers
{
    public interface IAdditionalRollProcess
    {
        Task<bool> CanRollAsync();
        Task BeforeRollingAsync();
    }
}