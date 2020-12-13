using CommonBasicStandardLibraries.CollectionClasses;
namespace ClueBoardGameCP.Data
{
    public class ComputerInfo
    {
        public CustomBasicList<GivenInfo> CluesGiven = new CustomBasicList<GivenInfo>();
        public CustomBasicList<ReceivedInfo> CluesReceived = new CustomBasicList<ReceivedInfo>();
        public string RoomHeaded = "";
        public string Weapon = ""; // this is the weapon the computer thinks it is
        public string Character = ""; // this is the character the computer thinks it is
    }
}