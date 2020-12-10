using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace CribbagePatienceBlazor
{
    public partial class ScoreSummaryUI
    {
        [Parameter]
        public CustomBasicList<int> Scores { get; set; } = new CustomBasicList<int>();

    }
}