using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    //this could be a good one to use records eventually.
    public class SimplePlayer : ObservableObject, IPlayerItem, IEquatable<SimplePlayer> //decided to make it observable.  does not hurt things.
    {
        public int Id { get; set; }
        private string _nickName = "";

        public string NickName
        {
            get { return _nickName; }
            set
            {
                if (SetProperty(ref _nickName, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
        private bool _inGame;
        public bool InGame
        {
            get
            {
                return _inGame;
            }

            set
            {
                if (SetProperty(ref _inGame, value) == true)
                {
                }
            }
        }
        private bool _isReady;
        public bool IsReady
        {
            get
            {
                return _isReady;
            }

            set
            {
                if (SetProperty(ref _isReady, value) == true)
                {
                }
            }
        }
        private bool _missNextTurn;

        public bool MissNextTurn
        {
            get { return _missNextTurn; }
            set
            {
                if (SetProperty(ref _missNextTurn, value))
                {
                    //can decide what to do when property changes
                }

            }
        }

        private EnumPlayerCategory _playerCategory;
        public EnumPlayerCategory PlayerCategory
        {
            get
            {
                return _playerCategory;
            }

            set
            {
                if (SetProperty(ref _playerCategory, value) == true)
                {
                }
            }
        }
        private bool _isHost;

        public bool IsHost
        {
            get { return _isHost; }
            set
            {
                if (SetProperty(ref _isHost, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
        public virtual bool CanStartInGame => true; //clue will have exceptions.
        public override bool Equals(object obj)
        {
            if (!(obj is SimplePlayer Temps))
            {
                return false;
            }
            return NickName.Equals(Temps.NickName);
        }
        public bool Equals(SimplePlayer other)
        {
            return NickName.Equals(other.NickName);
        }

        public override int GetHashCode()
        {
            return NickName.GetHashCode();
        }
    }
}