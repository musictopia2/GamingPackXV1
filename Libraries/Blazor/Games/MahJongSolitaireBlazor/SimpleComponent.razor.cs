using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace MahJongSolitaireBlazor
{
    public partial class SimpleComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool CanRender { get; set; } = true;
        [Parameter]
        public object? Key { get; set; }
        protected override bool ShouldRender()
        {
            return CanRender;
        }
    }
}