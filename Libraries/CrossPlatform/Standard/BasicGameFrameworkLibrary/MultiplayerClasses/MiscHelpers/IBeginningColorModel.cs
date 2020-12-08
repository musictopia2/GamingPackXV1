using BasicGameFrameworkLibrary.ChooserClasses;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers
{
    public interface IBeginningColorModel<E>
        where E : struct, Enum
    {
        SimpleEnumPickerVM<E> ColorChooser { get; } //maybe the second generic is not needed anymore (?)
    }
}