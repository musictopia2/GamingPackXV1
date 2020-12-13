using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
namespace SpellingLibrary
{
    public class SpellingLogic : IDisposable
    {

        public CustomBasicList<WordInfo> GetWords(EnumDifficulty? difficulty, int? letters)
        {
            if (_thisList == null)
            {
                GetList();
            }
            if (difficulty.HasValue == false && letters.HasValue == false)
            {
                return _thisList!.ToCustomBasicList();// to make a new list so if any get removed; won't be removed from the dll
            }
            if (difficulty.HasValue == false)
                return (from Items in _thisList
                        where Items.Letters == letters
                        select Items).ToCustomBasicList();
            if (letters.HasValue == false)
            {
                return _thisList.Where(Items => Items.Difficulty == difficulty!.Value).ToCustomBasicList();
            }
            return _thisList.Where(Items => Items.Difficulty == difficulty!.Value && Items.Letters == letters!.Value).ToCustomBasicList();
        }

        private CustomBasicList<WordInfo>? _thisList;

        private void GetList()
        {
            Assembly thisAssembly = Assembly.GetAssembly(GetType());
            string thisText = thisAssembly.ResourcesAllTextFromFile("spelling.json");
            _thisList = JsonConvert.DeserializeObject<CustomBasicList<WordInfo>>(thisText);
        }

        public void Dispose()
        {

        }
    }
}
