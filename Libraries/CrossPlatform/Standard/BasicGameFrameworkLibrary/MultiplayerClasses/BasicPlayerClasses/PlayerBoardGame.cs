using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public abstract class PlayerBoardGame<E> : SimplePlayer, IPlayerBoardGame<E>
        where E : struct, Enum
    {
        private E _color;
        public E Color
        {
            get { return _color; }
            set
            {
                if (SetProperty(ref _color, value)) { }
            }
        }
        public abstract bool DidChooseColor { get; }
        public abstract void Clear();
    }
}
