using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace Rummy500CP.Data
{
    [SingletonGame]
    public class CustomConfig : IRegularCardsSortCategory
    {
        public EnumRegularCardsSortCategory SortCategory => EnumRegularCardsSortCategory.NumberSuit;
    }
}