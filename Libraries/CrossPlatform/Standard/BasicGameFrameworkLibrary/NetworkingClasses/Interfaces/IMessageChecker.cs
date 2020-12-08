using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Interfaces
{
    public interface IMessageChecker
    {
        string NickName { get; set; } //anybody who wants to be a checker, must have a nick name.
        bool IsEnabled { get; set; } //this for sure is needed.
        Task InitAsync(); //we reserve the right for it to be async.
    }
}