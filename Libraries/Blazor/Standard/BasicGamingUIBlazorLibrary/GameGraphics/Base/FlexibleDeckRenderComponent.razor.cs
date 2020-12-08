using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BasicGamingUIBlazorLibrary.GameGraphics.Base
{
    public partial class FlexibleDeckRenderComponent<D>
        where D: class, IDeckObject
    {
        [Parameter]
        public D? DeckObject { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }


        [Parameter]
        public bool ConsiderEnabled { get; set; } = false; //most of the time, won't be considered.

        private BasicDeckRecordModel? _previous;

        protected override void OnAfterRender(bool firstRender)
        {
            _previous = GetRecord();
            //_previous = DeckObject!.GetRecord;
           

        }

        private BasicDeckRecordModel GetRecord()
        {
            var record = DeckObject!.GetRecord;
            BasicDeckRecordModel output;
            if (ConsiderEnabled == false)
            {
                output = record with
                {
                    IsEnabled = true
                };
            }
            else
            {
                output = record;
            }
            return output;
        }

        
        private bool CanRender => _previous != GetRecord();
        //private bool CanRender => !_previous!.Equals(GetRecord());
    }
}