using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
namespace BasicGameFrameworkLibrary.ChooserClasses
{
    public class ColorListChooser<E> : IEnumListClass<E> where E : Enum, new()
    {
        CustomBasicList<E> IEnumListClass<E>.GetEnumList()
        {
            E newE = new E();
            var output = newE.GetColorList();
            output.RemoveAllOnly(items => items.ToString() == "None"); //i don't think we want none either.  don't remember why i put it in there.
            return output;
        }
    }
}