using BasicGameFrameworkLibrary.MultiplePilesObservable;
using HeapSolitaireCP.Data;
using Microsoft.AspNetCore.Components;
using System;

namespace HeapSolitaireBlazor
{
    public partial class SinglePileUI
    {

        [Parameter]
        public BasicPileInfo<HeapSolitaireCardInfo>? Pile { get; set; }
        
        
    }
}