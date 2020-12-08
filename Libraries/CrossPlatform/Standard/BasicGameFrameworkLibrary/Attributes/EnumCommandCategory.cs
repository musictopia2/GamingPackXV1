namespace BasicGameFrameworkLibrary.Attributes
{
    public enum EnumCommandCategory
    {
        Plain = 1,
        Game,
        Limited, //this means starts as limited.  used for games like clue board game for clicking on your clues.
        OutOfTurn,
        Open,
        Control,
        Old //well see how this will work (?)
    }
}