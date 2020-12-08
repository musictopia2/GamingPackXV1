using System;

namespace BasicGameFrameworkLibrary.BasicGameDataClasses
{
    public class BasicData
    {

        //attempt with no wasm.  maybe not needed anymore since we have better performance now.


        //public static bool IsWasm { get; set; } = false; //this is a temporary solution.  allows anything to attempt optimisations
        public bool MultiPlayer { get; set; }
        public string NickName { get; set; } = "";

        private bool _gameDataLoading;

        public bool GameDataLoading
        {
            get { return _gameDataLoading; }
            set
            {
                _gameDataLoading = value;
                ChangeState?.Invoke();
            }
        }


        //public bool GameDataLoading { get; set; } = true;
        //may need xamarinforms since this will do xamarin forms from blazor (however, its all xamarin forms for this version anyways).

        //public bool IsXamarinForms { get; set; } //i think this is fine too.
        public bool Client { get; set; }
        public EnumGamePackageMode GamePackageMode { get; set; } = EnumGamePackageMode.None; //default to none.  will require showing what it is.


        public Action? ChangeState { get; set; }

    }
}