using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Linq;
using ThreeLetterFunCP.Data;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace ThreeLetterFunCP.Logic
{
    public class TileBoardObservable : HandObservable<TileInformation>
    {
        public TileBoardObservable(CommandContainer command) : base(command)
        {
        }
        public void UpdateBoard()
        {
            ThreeLetterFunSaveInfo saveroot = cons!.Resolve<ThreeLetterFunSaveInfo>();
            if (saveroot.TileList.Count == 0)
            {
                HandList.Clear();
                return;
            }
            CustomBasicList<TileInformation> ThisList = new CustomBasicList<TileInformation>
                { saveroot.TileList.First(), saveroot.TileList[1] };
            HandList.ReplaceRange(ThisList);
        }
        public TileInformation? GetTile(bool isSelected)
        {
            if (isSelected)
            {
                if (HandList.First().IsSelected == true)
                {
                    return HandList.First();
                }
                else if (HandList[1].IsSelected == true)
                {
                    return HandList[1];
                }
                else
                {
                    return null;
                }
            }
            else if (HandList.First().IsSelected == false)
            {
                return HandList.First();
            }
            else if (HandList[1].IsSelected == false)
            {
                return HandList[1];
            }
            else
            {
                return null;
            }
        }
        public void Undo()
        {
            UnselectAllObjects();
            ThreeLetterFunSaveInfo saveroot = cons!.Resolve<ThreeLetterFunSaveInfo>(); //to stop the overflow issues by requiring reference to game class
            saveroot.TileList.ForEach(x => x.Visible = true);
        }
    }
}