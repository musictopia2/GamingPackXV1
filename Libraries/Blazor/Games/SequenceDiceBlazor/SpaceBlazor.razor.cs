using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using BasicGameFrameworkLibrary.Dice;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using Microsoft.AspNetCore.Components;
using SequenceDiceCP.Data;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace SequenceDiceBlazor
{
    public partial class SpaceBlazor : GraphicsCommand
    {
        private SpaceInfoCP? _space;

        protected override void OnInitialized()
        {
            if (CommandParameter != null)
            {
                _space = (SpaceInfoCP)CommandParameter;
            }

            base.OnInitialized();
        }

        private string GetFillRegular
        {
            get
            {
                if (_space!.WasRecent)
                {
                    return "Yellow";
                }
                return "White";
            }
        }
        private string GetFillDice
        {
            get
            {
                if (_space!.WasRecent)
                {
                    return cc.Yellow;
                }
                return cc.White;
            }
        }

        private SimpleDice GetDiceInfo()
        {
            SimpleDice output = new SimpleDice();
            output.Populate(_space!.Number / 2);
            output.FillColor = GetFillDice;
            return output;
        }

        private PointF GetDiceLocation(int index)
        {
            if (index == 1)
            {
                return new PointF(2, 2);
            }
            return new PointF(26, 26);
        }

        private SizeF GetDiceSize => new SizeF(21, 21); //try this.
        
        private double FontSize()
        {
            if (_space!.Number == 12)
            {
                return 30;
            }
            return 47;
        }

        //private int DiceValue => _space!.Number / 2;


    }
}