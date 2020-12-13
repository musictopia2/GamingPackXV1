using CommonBasicStandardLibraries.CollectionClasses;
using CribbageCP.Data;
using Microsoft.AspNetCore.Components;
namespace CribbageBlazor
{
    public partial class ScoreUI
    {
        [Parameter]
        public CustomBasicList<ScoreInfo>? ScoreList { get; set; }
    }
}