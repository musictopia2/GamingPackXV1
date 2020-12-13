namespace FluxxCP.Data
{
    public enum EnumActionScreen
    {
        None = 0,
        ActionScreen = 1,
        KeeperScreen = 2, // would be better each time to do a select/case to figure out which controls to load up
        OtherPlayer = 3 // for taxation; the other players chooses a card from the main screen.  still a 2 parter
    }
}