using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BasicGamingUIBlazorLibrary.StartupClasses
{
    public static class GlobalStartUp
    {
        //public static EnumGamePackageMode GamePackageMode { get; set; }

        //public static Func<StartRecord>? GetStartInfo { get; set; }

        public static Action? StartBootStrap { get; set; }

        public static IJSRuntime? JsRuntime { get; set; }


        private static CustomBasicList<string> _keys = new();

        public static CustomBasicList<string> KeysToSave => _keys;

        

    }
}
