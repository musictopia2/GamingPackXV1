using BasicGameFrameworkLibrary.CommonInterfaces;
namespace BasicGameFrameworkLibrary.GamePieceModels
{
    public class NumberModel : ISimpleValueObject<int>, ISelectableObject, IEnabledObject
    {
        public int NumberValue { get; set; } = -1; //defaults to -1 which means nothing
        public bool IsEnabled { get; set; } //you have to manually show its not enabled.
        public bool IsSelected { get; set; }
        int ISimpleValueObject<int>.ReadMainValue => NumberValue;
    }
}