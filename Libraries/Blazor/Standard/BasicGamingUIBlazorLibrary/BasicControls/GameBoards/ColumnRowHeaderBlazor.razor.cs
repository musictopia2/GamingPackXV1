using BasicGameFrameworkLibrary.GameBoardCollections;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    public partial class ColumnRowHeaderBlazor<S>
        where S : class, IBasicSpace, new()
    {
        [CascadingParameter]
        private GridGameBoard<S>? Board { get; set; }
        protected override bool ShouldRender()
        {
            return false; //this part should never have to rerender again.  hopefully will help with problems with battleship
        }
    }
}