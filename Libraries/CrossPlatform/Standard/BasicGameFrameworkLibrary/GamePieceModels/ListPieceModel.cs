using BasicGameFrameworkLibrary.CommonInterfaces;
namespace BasicGameFrameworkLibrary.GamePieceModels
{
    public class ListPieceModel : ISimpleValueObject<int>, ISelectableObject, IEnabledObject
    {
        public int Index { get; set; }
        public string DisplayText { get; set; } = ""; //hopefully this is all that is needed (?)
        public bool IsEnabled { get; set; } //you have to manually show its not enabled.
        public bool IsSelected { get; set; }
        int ISimpleValueObject<int>.ReadMainValue => Index;
    }
}