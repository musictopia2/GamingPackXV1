using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic
{
    public interface IYahtzeeMove
    {
        Task MakeMoveAsync(RowInfo row);
    }
}