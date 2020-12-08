using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels
{
    public interface IScoresheetAction
    {
        Task RowAsync(RowInfo row);
    }
}