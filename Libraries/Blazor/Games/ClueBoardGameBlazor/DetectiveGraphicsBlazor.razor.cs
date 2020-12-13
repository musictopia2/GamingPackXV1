using ClueBoardGameCP.Data;
using ClueBoardGameCP.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using pp = ClueBoardGameBlazor.GlobalDetectiveGrid;
namespace ClueBoardGameBlazor
{
    public partial class DetectiveGraphicsBlazor
    {
        [CascadingParameter]
        public EnumDetectiveCategory DetectiveCategory { get; set; }
        [CascadingParameter]
        public ClueBoardGameMainViewModel? DataContext { get; set; }
        [Parameter]
        public Dictionary<int, DetectiveInfo>? DetectiveList { get; set; }
        private CustomBasicList<DetectiveInfo> _roomList = new CustomBasicList<DetectiveInfo>();
        private CustomBasicList<DetectiveInfo> _weaponList = new CustomBasicList<DetectiveInfo>();
        private CustomBasicList<DetectiveInfo> _characterList = new CustomBasicList<DetectiveInfo>();
        protected override void OnParametersSet()
        {
            if (DetectiveList == null)
            {
                return;
            }
            _roomList = DetectiveList.Values.Where(x => x.Category == EnumCardType.IsRoom).ToCustomBasicList();
            _weaponList = DetectiveList.Values.Where(x => x.Category == EnumCardType.IsWeapon).ToCustomBasicList();
            _characterList = DetectiveList.Values.Where(x => x.Category == EnumCardType.IsCharacter).ToCustomBasicList();
            base.OnParametersSet();
        }
        private int StartX(EnumCardType category)
        {
            return category switch
            {
                EnumCardType.IsRoom => 0,
                EnumCardType.IsWeapon => pp.RoomWidth + pp.CharacterWidth,
                EnumCardType.IsCharacter => pp.RoomWidth,
                _ => -10,
            };
        }
        private int ColumnWidth(EnumCardType category)
        {
            return category switch
            {
                EnumCardType.IsRoom => pp.RoomWidth,
                EnumCardType.IsWeapon => pp.RoomWidth,
                EnumCardType.IsCharacter => pp.CharacterWidth,
                _ => 10
            };
        }
        private RectangleF GetButtonLocation(EnumCardType category, int row) //sent 0 based.
        {
            int startY = pp.CellHeight * row;
            int startX = StartX(category);
            int width = ColumnWidth(category);
            return new RectangleF(startX, startY, width, pp.CellHeight);
        }
        private int GetIndex(DetectiveInfo detective)
        {
            return detective.Category switch
            {
                EnumCardType.IsRoom => _roomList.IndexOf(detective),
                EnumCardType.IsWeapon => _weaponList.IndexOf(detective),
                EnumCardType.IsCharacter => _characterList.IndexOf(detective),
                _ => -1
            };
        }
        private RectangleF GetButtonLocation(DetectiveInfo detective)
        {

            int row = GetIndex(detective);
            return GetButtonLocation(detective.Category, row);
        }
    }
}