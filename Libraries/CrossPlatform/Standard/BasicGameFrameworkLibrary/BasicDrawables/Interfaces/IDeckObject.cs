using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using System.Drawing;
namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    public interface IDeckObject : ICommonObject, IPopulateObject<int>
    {
        int Deck { get; set; }
        bool Drew { get; set; }
        bool IsUnknown { get; set; }
        bool Rotated { get; set; } //for now, only true/false.  hopefully that is all that is needed.
        SizeF DefaultSize { get; set; } //hopefully this will work.  will be helpful in doing the controls for the cards.
        void Reset(); //sometimes needs to be reset.  everything can need it.
        /// <summary>
        /// this is needed to return a record.
        /// intended to be used to first return a record after something renders.
        /// then return again to determine whether to render so it knows whether to render to help in performance.  especially with blazor.
        /// </summary>
        /// <returns></returns>
        BasicDeckRecordModel GetRecord { get; }
        /// <summary>
        /// this is needed because many things needs a key to determine whether to dispose to recreate for cases that it gets updated.
        /// </summary>
        /// <returns></returns>
        string GetKey();
    }
}