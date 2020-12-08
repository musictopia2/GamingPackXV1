namespace BasicGameFrameworkLibrary.DrawableListsObservable
{
    public enum EnumHandAutoType
    {
        None = 0,
        SelectOneOnly = 1,
        SelectAsMany = 2,
        ShowObjectOnly = 3 // this means it will raise the event for considering but nothing else.  the purpose of this is for games like fluxx where you need to see what the card is (extra details).  since there is no mouse move.
    }
}