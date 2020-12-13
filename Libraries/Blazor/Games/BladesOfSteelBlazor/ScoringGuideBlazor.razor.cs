using BladesOfSteelCP.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using aa = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BladesOfSteelBlazor
{
    public partial class ScoringGuideBlazor
    {
        private ScoringGuideClassCP? _data;
        private CustomBasicList<string> _offenseList = new CustomBasicList<string>();
        private CustomBasicList<string> _defenseList = new CustomBasicList<string>();
        protected override void OnInitialized()
        {
            _data = new ScoringGuideClassCP();
            _offenseList = _data.OffenseList();
            _defenseList = _data.DefenseList();
            base.OnInitialized();
        }
        private string GetColumns()
        {
            return $"{aa.Auto} {aa.Auto}";
        }
    }
}