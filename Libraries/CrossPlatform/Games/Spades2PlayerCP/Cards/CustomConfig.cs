using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace Spades2PlayerCP.Cards
{
    [SingletonGame]
    public class CustomConfig : IRegularCardsSortCategory
    {
        public EnumRegularCardsSortCategory SortCategory => EnumRegularCardsSortCategory.SuitNumber;
    }
}