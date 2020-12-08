using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces
{
    /// <summary>
    /// if special things has to happen when its finished starting, will be here.
    /// crazy eights for example does have something special.
    /// </summary>
    public interface IFinishStart
    {
        Task FinishStartAsync();
    }
}