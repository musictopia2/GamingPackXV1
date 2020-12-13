using CandylandCP.Data;
using System;
using System.Collections.Generic;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace CandylandBlazor
{
    public static class Extensions
    {
        public static Dictionary<int, CandylandSquareModel> AddSquare(this Dictionary<int, CandylandSquareModel> squares,
            float firstWidth, float firstHeight,
            float secondWidth, float secondHeight,
            float thirdWidth, float thirdHeight,
            float fourthWidth, float fourthHeight
            )
        {

            squares.Add(squares.Count + 1, new CandylandSquareModel()
            {
                FirstWidth = firstWidth,
                FirstHeight = firstHeight,
                SecondWidth = secondWidth,
                SecondHeight = secondHeight,
                ThirdWidth = thirdWidth,
                ThirdHeight = thirdHeight,
                FourthWidth = fourthWidth,
                FourthHeight = fourthHeight
            });
            return squares;
        }
        public static string GetColor(this CandylandPlayerItem player)
        {
            return GetColor(player.Id);
        }
        private static string GetColor(int index)
        {
            return index switch
            {
                1 => cs.Aqua,
                2 => cs.LimeGreen,
                3 => cs.Orange,
                4 => cs.Gray,
                _ => throw new Exception("Only 1 to 4 are supported for piece color"),
            };
        }
    }
}