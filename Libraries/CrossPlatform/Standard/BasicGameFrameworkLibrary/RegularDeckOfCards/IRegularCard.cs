using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public interface IRegularCard : IDeckObject, ISimpleValueObject<int>, IWildObject
        , ISuitObject<EnumSuitList>, IColorObject<EnumRegularColorList>, IAdvancedDIContainer
    {
        EnumRegularColorList Color { get; set; }
        EnumSuitList Suit { get; set; }
        EnumRegularCardValueList Value { get; set; }
        EnumSuitList DisplaySuit { get; set; }
        EnumRegularCardValueList DisplayNumber { get; set; } //this means it can show differently than what it really is.
        int Section { get; }
        EnumRegularCardTypeList CardType { get; set; } //this is needed after all.
    }
}