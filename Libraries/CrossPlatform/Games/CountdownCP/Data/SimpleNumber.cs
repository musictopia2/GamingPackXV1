using Newtonsoft.Json;
using System.Drawing;
namespace CountdownCP.Data
{
    public class SimpleNumber //no need for observable this time.  since its repainting everything this time.
    {
        public int Value { get; set; }
        public bool Used { get; set; }
        public bool IsSelected { get; set; }
        [JsonIgnore]
        public PointF Location { get; set; } //this is needed for blazor to position the items.
    }
}