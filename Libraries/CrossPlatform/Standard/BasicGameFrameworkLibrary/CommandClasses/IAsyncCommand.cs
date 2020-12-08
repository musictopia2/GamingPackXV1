using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public interface IAsyncCommand //this one will require eventually putting into the common standard library.
    {
        bool CanExecute(object? parameter);
        Task ExecuteAsync(object? parameter);
    }
}