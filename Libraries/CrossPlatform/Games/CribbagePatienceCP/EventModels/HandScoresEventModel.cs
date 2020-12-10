using CommonBasicStandardLibraries.CollectionClasses;
using CribbagePatienceCP.Data;
namespace CribbagePatienceCP.EventModels //i did need the handscoreeventmodel after all for the view model.
{
    public class HandScoresEventModel
    {
        public CustomBasicList<ScoreHandCP> HandScores;
        public HandScoresEventModel(CustomBasicList<ScoreHandCP> handScores)
        {
            HandScores = handScores;
        }
    }
}