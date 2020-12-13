using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace FourSuitRummyCP.Data
{
    [SingletonGame]
    public class CustomConfig : IRegularCardsSortCategory
    {
        public EnumRegularCardsSortCategory SortCategory => EnumRegularCardsSortCategory.NumberSuit;
    }
}