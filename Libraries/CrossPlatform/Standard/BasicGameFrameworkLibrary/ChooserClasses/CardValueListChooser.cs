using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.ChooserClasses
{
    public class CardValueListChooser : IEnumListClass<EnumRegularCardValueList>
    {
        CustomBasicList<EnumRegularCardValueList> IEnumListClass<EnumRegularCardValueList>.GetEnumList()
        {
            return new CustomBasicList<EnumRegularCardValueList>()
            {
                EnumRegularCardValueList.LowAce, EnumRegularCardValueList.Two, EnumRegularCardValueList.Three, EnumRegularCardValueList.Four,
                EnumRegularCardValueList.Five, EnumRegularCardValueList.Six, EnumRegularCardValueList.Seven, EnumRegularCardValueList.Eight,
                EnumRegularCardValueList.Nine, EnumRegularCardValueList.Ten, EnumRegularCardValueList.Jack, EnumRegularCardValueList.Queen,
                EnumRegularCardValueList.King
            };
        }
    }
}