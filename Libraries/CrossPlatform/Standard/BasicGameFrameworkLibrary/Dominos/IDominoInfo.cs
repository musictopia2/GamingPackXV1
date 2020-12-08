using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
namespace BasicGameFrameworkLibrary.Dominos
{
    /// <summary>
    /// this is everything that is needed to be a basic domino.
    /// the dicontainer is needed so it can populate the domino.
    /// </summary>
    public interface IDominoInfo : IDeckObject, ILocationDeck
    {
        int FirstNum { get; set; }
        int SecondNum { get; set; } //was going to not abbreviate but did not feel like making lots of breaking changes just to not have abbreviations
        int CurrentFirst { get; set; }
        int CurrentSecond { get; set; } //these 2 are needed so it can know how to display it.
        int Points { get; }
        int HighestDomino { get; } //i think that is best.
    }
}