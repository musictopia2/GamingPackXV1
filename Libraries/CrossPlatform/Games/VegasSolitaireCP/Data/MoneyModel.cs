using BasicGameFrameworkLibrary.Attributes;
namespace VegasSolitaireCP.Data
{
    [SingletonGame]
    public class MoneyModel
    {
        public decimal Money { get; set; } = 500;
    }
}