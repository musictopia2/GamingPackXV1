using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.Data;
using LifeBoardGameCP.Graphics;
using LifeBoardGameCP.Logic;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace LifeBoardGameBlazor
{
    public partial class LifeCompleteGameBoardBlazor
    {
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public GameBoardGraphicsCP? GraphicsData { get; set; }
        [CascadingParameter]
        public LifeBoardGameGameContainer? ContainerData { get; set; } //hopefully this works.
        [Parameter]
        public IBoardProcesses? BoardProcesses { get; set; }


        private string GetPortion()
        {
            return ContainerData!.CurrentView switch
            {
                EnumViewCategory.StartGame => "matrix(0.888889 0 0 0.666667 -26.6667 0)",
                EnumViewCategory.SpinAfterHouse => "matrix(0.888889 0 0 0.666667 -204.444 -233.333)",
                EnumViewCategory.AfterFirstSetChoices => "matrix(0.888889 0 0 0.666667 -35.5556 -220)",
                EnumViewCategory.EndGame => "matrix(0.888889 0 0 0.666667 -266.667 0)",
                _ => "",
            };
        }
        private PointF GetTurnPoint()
        {
            int index = (int)ContainerData!.CurrentView + 400;
            var position = GraphicsData!.TempData.PositionList.Where(x => x.SpaceNumber == index).Single();
            return position.SpacePoint;
        }
        private PointF _currentPoint;
        private PositionInfo? GetPlayerPosition(LifeBoardGamePlayerItem player)
        {
            _currentPoint = new PointF(0, 0);
            if (player.LastMove == EnumFinal.None && player.OptionChosen != EnumStart.None && player.Position > 0)
            {
                _currentPoint = CalculateView();
                return GraphicsData!.TempData.PositionList.SingleOrDefault(x => x.PointView == _currentPoint && x.SpaceNumber == player.Position);
            }
            if (player.OptionChosen == EnumStart.Career && player.LastMove == EnumFinal.None)
            {
                return GraphicsData!.TempData.PositionList.Where(x => x.SpaceNumber == 201).Single();
            }
            if (player.OptionChosen == EnumStart.College && player.LastMove == EnumFinal.None)
            {
                return GraphicsData!.TempData.PositionList.Where(x => x.SpaceNumber == 202).Single();
            }
            return null;
        }
        private PointF CalculateView()
        {
            switch (ContainerData!.CurrentView)
            {
                case EnumViewCategory.StartGame:
                    {
                        return new PointF(30, 0);
                    }
                case EnumViewCategory.SpinAfterHouse:
                    {
                        return new PointF(230, 350);
                    }
                case EnumViewCategory.AfterFirstSetChoices:
                    {
                        return new PointF(40, 330);
                    }
                case EnumViewCategory.EndGame:
                    {
                        return new PointF(300, 0);
                    }
                default:
                    {
                        return new PointF(0, 0);
                    }
            }
        }
        private void SpaceChosenAsync(int space)
        {
            if (ContainerData!.Command.IsExecuting)
            {
                return;
            }
            BoardProcesses!.SpaceDescription(space);
        }
        private Assembly GetAssembly => Assembly.GetAssembly(GetType());
        private CustomBasicList<ButtonInfo> GetMainButtons()
        {
            return GraphicsData!.GetMainActions();
        }
        private async Task PerformAction(ButtonInfo button)
        {
            if (ContainerData!.Command.IsExecuting)
            {
                return;
            }
            await button.Action!.Invoke();
        }
        private async Task StartActions(EnumStart start)
        {
            if (ContainerData!.Command.IsExecuting)
            {
                return;
            }
            ContainerData.Command.StartExecuting();
            await BoardProcesses!.OpeningOptionAsync(start);
        }
        private async Task RetirementActions(EnumFinal final)
        {
            if (ContainerData!.Command.IsExecuting)
            {
                return;
            }
            ContainerData.Command.StartExecuting();
            await BoardProcesses!.RetirementAsync(final);
        }
        protected override bool ShouldRender()
        {
            return ContainerData!.SingleInfo!.Color != EnumColorChoice.None;
        }
    }
}