using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.CollectionClasses;
using CountdownCP.Data;
using System.Drawing;
namespace CountdownCP.Graphics
{
    [InstanceGame]
    public class PlayerBoardCP
    {
        public readonly CountdownGameContainer GameContainer;
        private readonly CustomBasicList<SimpleNumber> _thisList = new CustomBasicList<SimpleNumber>();
        public PlayerBoardCP(CountdownGameContainer gameContainer)
        {
            GameContainer = gameContainer;
        }
        public SizeF OriginalSize = new SizeF(320, 100);
        public SizeF SpaceSize = new SizeF(60, 40);
        private void CreateSpaces(CountdownPlayerItem player)
        {
            if (player.NumberList.Count != 10)
            {
                return;
            }
            int x;
            int y;
            int z = default;
            float tops = 0;
            for (x = 1; x <= 2; x++)
            {
                float lefts = 8;
                for (y = 1; y <= 5; y++)
                {
                    var tempNumber = player.NumberList[z];
                    tempNumber.Location = new PointF(lefts, tops);
                    lefts += SpaceSize.Width + 3;
                    z += 1;
                }
                tops += SpaceSize.Height + 3;
            }
        }
        public void UpdatePlayer(CountdownPlayerItem player)
        {
            if (player.NumberList.Count == 0)
            {
                player.NumberList = GameContainer.GetNumberList!.Invoke();
            }
            _thisList.Clear();
            CreateSpaces(player); //try to create spaces again.
        }
        //has to be public because blazor needs it.
        public bool CanClickOnPlayer(CountdownPlayerItem player)
        {
            if (GameContainer.Command.IsExecuting)
            {
                return false;
            }
            var id = player.Id;
            return id == GameContainer.SaveRoot.PlayOrder.WhoTurn;
        }
    }
}