using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using System.Drawing;
namespace BasicGamingUIBlazorLibrary.Extensions
{
    public static class SizeExtensions
    {
        public static string HeightString(this int heightRequest)
        {
            return $"{heightRequest}vh";
        }
        public static string WidthString<D>(this D deck, int heightRequest)
            where D : IDeckObject
        {
            double starts = heightRequest * deck.DefaultSize.Width / deck.DefaultSize.Height;
            return $"{starts}vh"; //i think
        }
        public static string WidthString(this SizeF size, int heightRequest)
        {
            double starts = heightRequest * size.Width / size.Height;
            return $"{starts}vh"; //i think
        }

        public static string WidthString<D>(this int heightRequest)
            where D : IDeckObject, new()
        {
            D obj = new D();
            return obj.WidthString(heightRequest);
        }
    }
}