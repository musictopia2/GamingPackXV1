using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Collections.Generic;
using System.Text;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace ThreeLetterFunBlazor
{
    public static class Extensions
    {
        private static readonly CustomBasicList<string> _vowelList = new CustomBasicList<string>() { "A", "E", "I", "O", "U" };
        public static string GetColorOfLetter(this string thisLetter)
        {
            if (_vowelList.Count != 5)
                throw new Exception("Must have 5 vowels");
            if (_vowelList.Exists(x => x == thisLetter))
                return cc.Red;
            return cc.Black;
        }
    }
}
