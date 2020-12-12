using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.JSInterop;
using System;
namespace BasicGamingUIBlazorLibrary.StartupClasses
{
    public static class GlobalStartUp
    {
        public static Action? StartBootStrap { get; set; }
        public static IJSRuntime? JsRuntime { get; set; }

        private readonly static CustomBasicList<string> _keys = new();
        public static CustomBasicList<string> KeysToSave => _keys;
    }
}