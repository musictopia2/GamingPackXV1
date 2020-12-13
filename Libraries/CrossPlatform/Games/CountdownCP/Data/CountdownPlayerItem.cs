using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace CountdownCP.Data
{
    public class CountdownPlayerItem : SimplePlayer
    {
        public CustomBasicList<SimpleNumber> NumberList { get; set; } = new CustomBasicList<SimpleNumber>();
    }
}