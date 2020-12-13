using System.Collections.Generic;
namespace ClueBoardGameCP.Data
{
    public class FieldInfo
    {
        public Dictionary<int, MoveInfo> Neighbors = new Dictionary<int, MoveInfo>();
    }
}