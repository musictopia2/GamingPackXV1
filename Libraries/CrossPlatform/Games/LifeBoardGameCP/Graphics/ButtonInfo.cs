using System;
using System.Drawing;
using System.Threading.Tasks;
namespace LifeBoardGameCP.Graphics
{
    public class ButtonInfo
    {
        public PointF Location { get; set; }
        public Func<Task>? Action { get; set; }
        public string Display { get; set; } = "";
        public SizeF Size { get; set; }
    }
}