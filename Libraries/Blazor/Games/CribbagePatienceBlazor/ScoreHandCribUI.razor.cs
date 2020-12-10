using System;
using System.Linq;
using System.Net.Http;
using CribbagePatienceCP.Data;
using Microsoft.AspNetCore.Components;

namespace CribbagePatienceBlazor
{
    public partial class ScoreHandCribUI
    {
        [Parameter]
        public ScoreHandCP? Score { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 0;

        private string HeightString => $"{TargetHeight}vh";
    }
}