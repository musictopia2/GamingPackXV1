using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public class PlayerTrick<S, T> : PlayerSingleHand<T>, IPlayerTrick<S, T>
        where S : Enum
        where T : ITrickCard<S>, new()
    {
        private int _tricksWon;

        public int TricksWon
        {
            get { return _tricksWon; }
            set
            {
                if (SetProperty(ref _tricksWon, value))
                {
                    //can decide what to do when property changes
                }

            }
        }

    }
}