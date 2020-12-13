using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using RageCardGameCP.Data;
namespace RageCardGameBlazor
{
    public static class ScoreModule
    {
        public static CustomBasicList<ScoreColumnModel> GetScores()
        {
            CustomBasicList<ScoreColumnModel> output = new CustomBasicList<ScoreColumnModel>();
            output.AddColumn("Cards Left", true, nameof(RageCardGamePlayerItem.ObjectCount))
                .AddColumn("Bid Amount", true, nameof(RageCardGamePlayerItem.BidAmount), visiblePath: nameof(RageCardGamePlayerItem.RevealBid))
                .AddColumn("Tricks Won", true, nameof(RageCardGamePlayerItem.TricksWon))
                .AddColumn("Correctly Bidded", true, nameof(RageCardGamePlayerItem.CorrectlyBidded))
                .AddColumn("Score Round", true, nameof(RageCardGamePlayerItem.ScoreRound))
                .AddColumn("Score Game", true, nameof(RageCardGamePlayerItem.ScoreGame));
            return output;
        }
    }
}