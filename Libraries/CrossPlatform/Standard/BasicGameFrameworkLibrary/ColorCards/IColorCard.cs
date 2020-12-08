using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommonInterfaces;
namespace BasicGameFrameworkLibrary.ColorCards
{
    public interface IColorCard : IDeckObject, ISimpleValueObject<int>
        , IColorObject<EnumColorTypes>
    {
        EnumColorTypes Color { get; set; }
        string Display { get; set; } //this is needed so the ui can draw properly what it is
    }
}