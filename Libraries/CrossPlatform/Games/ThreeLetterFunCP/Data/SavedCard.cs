using CommonBasicStandardLibraries.CollectionClasses;
namespace ThreeLetterFunCP.Data
{
    public class SavedCard
    {
        public CustomBasicList<char> CharacterList { get; set; } = new CustomBasicList<char>();
        public EnumLevel Level { get; set; }
    }
}