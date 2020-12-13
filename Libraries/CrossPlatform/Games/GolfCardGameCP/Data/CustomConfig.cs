using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace GolfCardGameCP.Data
{
    [SingletonGame]
    public class CustomConfig : IRegularCardsSortCategory
    {
        public EnumRegularCardsSortCategory SortCategory => EnumRegularCardsSortCategory.SuitNumber;
    }
}