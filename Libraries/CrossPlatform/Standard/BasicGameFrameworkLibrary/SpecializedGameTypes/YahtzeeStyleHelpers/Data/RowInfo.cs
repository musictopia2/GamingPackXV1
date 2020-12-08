using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data
{
    public class RowInfo : ObservableObject
    {
        private string _description = "";
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (SetProperty(ref _description, value) == true) { }
            }
        }
        private int _rowNumber;
        public int RowNumber
        {
            get
            {
                return _rowNumber;
            }
            set
            {
                if (SetProperty(ref _rowNumber, value) == true) { }
            }
        }
        private EnumRow _rowSection;
        public EnumRow RowSection
        {
            get
            {
                return _rowSection;
            }
            set
            {
                if (SetProperty(ref _rowSection, value) == true) { }
            }
        }
        private bool _isTop;
        public bool IsTop
        {
            get
            {
                return _isTop;
            }
            set
            {
                if (SetProperty(ref _isTop, value) == true) { }
            }
        }
        private bool _isRecent;
        public bool IsRecent
        {
            get
            {
                return _isRecent;
            }
            set
            {
                if (SetProperty(ref _isRecent, value) == true) { }
            }
        }
        private int? _possible;
        public int? Possible
        {
            get
            {
                return _possible;
            }
            set
            {
                if (SetProperty(ref _possible, value) == true) { }
            }
        }
        private int? _pointsObtained;
        public int? PointsObtained
        {
            get
            {
                return _pointsObtained;
            }
            set
            {
                if (SetProperty(ref _pointsObtained, value) == true) { }
            }
        }
        internal bool IsAllFive()
        {
            if (Description == "Kismet (5 Of A Kind)" || Description == "Yahtzee")
            {
                return true;
            }
            return false;
        }
        public bool HasFilledIn()
        {
            if (RowSection == EnumRow.Header || RowSection == EnumRow.Totals)
            {
                throw new Exception("HasFilledIn can only be figured out for Bonus or Regular Rows");
            }
            if (PointsObtained.HasValue == false)
            {
                return false;
            }
            return true;
        }
        public void ClearText()
        {
            Possible = default;
            PointsObtained = default;
            IsRecent = false;
        }
        public void ClearPossibleScores()
        {
            Possible = default;
        }
        public RowInfo(EnumRow section, bool isTop)
        {
            RowSection = section;
            IsTop = isTop;
        }
        public RowInfo() { }
    }
}