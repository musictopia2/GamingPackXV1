using BasicGameFrameworkLibrary.GamePieceModels;
using System;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers
{
    public class CheckerChessPieceCP<E> : BasicPickerData<E>
        where E : struct, Enum
    {
        public bool Highlighted { get; set; } //only checkers need this.  but might as well have here anyways.
    }
}