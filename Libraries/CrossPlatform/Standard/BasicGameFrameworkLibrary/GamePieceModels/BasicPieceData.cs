using System;
namespace BasicGameFrameworkLibrary.GamePieceModels
{
    /// <summary>
    /// this is intended for crossplatform for pickers.  since the part that varies is the enumvalue.  can even be used from things like suit pickers
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class BasicPickerData<E> //i think this may have to go to a different namespace.  will do for next version.
       where E : struct, Enum
    {
        public E EnumValue { get; set; } //if color, can use the tostring.
        public bool IsEnabled { get; set; }
        public bool IsSelected { get; set; }
    }
}