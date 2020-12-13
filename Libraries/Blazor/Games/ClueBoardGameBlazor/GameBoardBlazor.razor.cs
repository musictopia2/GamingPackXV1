using ClueBoardGameCP.Logic;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Threading.Tasks;
namespace ClueBoardGameBlazor
{
    public partial class GameBoardBlazor
    {
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public GameBoardGraphicsCP? GraphicsData { get; set; }
        private Assembly GetAssembly => Assembly.GetAssembly(GetType());
        private async Task SpaceClickedAsync(int space)
        {
            await GraphicsData!.GameContainer!.ProcessCustomCommandAsync(GraphicsData.GameContainer.SpaceClickedAsync!, space);
        }
        private async Task RoomClickedAsync(int room)
        {
            await GraphicsData!.GameContainer!.ProcessCustomCommandAsync(GraphicsData.GameContainer.RoomClickedAsync!, room);
        }
        //try the standard key.  if that works, then can remove this part.

        //private RoomWeaponKeyClass GetWeaponKey(WeaponInfo weapon, PointF point)
        //{
        //    return new RoomWeaponKeyClass()
        //    {
        //        Weapon = weapon,
        //        Location = point
        //    };
        //}
        //private CharacterStartKey GetCharacterStartKey(CharacterInfo character)
        //{
        //    return new CharacterStartKey()
        //    {
        //        Character = character,
        //        StartSpace = character.FirstSpace
        //    };
        //}
        //private CharacterSpaceKey GetCharacterSpaceKey(CharacterInfo character, int space)
        //{
        //    return new CharacterSpaceKey()
        //    {
        //        Character = character,
        //        Space = space
        //    };
        //}
        //private CharacterRoomKey GetCharacterRoomKey(CharacterInfo character, int room)
        //{
        //    return new CharacterRoomKey()
        //    {
        //        Character = character,
        //        RoomNumber = room
        //    };
        //}
    }
}