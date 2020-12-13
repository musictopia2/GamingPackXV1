using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace GalaxyCardGameCP.Cards
{
    [SingletonGame]
    public class CustomConfig : IRegularCardsSortCategory
    {
        public EnumRegularCardsSortCategory SortCategory => EnumRegularCardsSortCategory.SuitNumber;
    }
}