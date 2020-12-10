using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace HeapSolitaireBlazor
{
    public partial class TestComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}