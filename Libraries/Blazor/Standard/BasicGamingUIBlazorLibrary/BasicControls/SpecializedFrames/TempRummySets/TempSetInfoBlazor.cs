using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using System;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.TempRummySets
{
    public class TempSetInfoBlazor<SU, CO, RU>
        where SU : Enum
        where CO : Enum
        where RU : class, IRummmyObject<SU, CO>, IDeckObject, new()
    {
        public RU? Image { get; set; }
        public HandObservable<RU>? Hand { get; set; }
    }
}