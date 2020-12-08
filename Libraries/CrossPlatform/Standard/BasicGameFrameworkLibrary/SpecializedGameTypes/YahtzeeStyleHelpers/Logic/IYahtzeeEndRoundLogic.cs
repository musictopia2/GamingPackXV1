using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic
{
    public interface IYahtzeeEndRoundLogic
    {
        Task StartNewRoundAsync();
        bool IsGameOver { get; }
    }
}