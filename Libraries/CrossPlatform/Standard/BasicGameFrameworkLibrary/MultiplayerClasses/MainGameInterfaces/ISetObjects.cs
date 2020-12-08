using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces
{
    public interface ISetObjects
    {
        /// <summary>
        /// You have to set the object of the saveroot to whatever you need.
        /// some games has nothing but others do.  
        /// </summary>
        /// <returns></returns>
        Task SetSaveRootObjectsAsync();
    }
}