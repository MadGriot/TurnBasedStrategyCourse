using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedStrategyCourse
{
    public class PathNode : IComparable<PathNode>
    {
        internal GridPosition gridPosition;
        internal int gCost;
        internal int hCost;
        internal int fCost { get; private set; }
        internal PathNode cameFromPathNode;
        public PathNode(GridPosition gridPosition)
        {
            this.gridPosition = gridPosition;
        }

        public override string ToString()
        {
            return gridPosition.ToString();
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
        public void SetCameFromPathNode(PathNode pathNode)
        {
            cameFromPathNode = pathNode;
        }
        public void ResetCameFromPathNode()
        {
            cameFromPathNode = null;
        }

        public int CompareTo(PathNode other)
        {
            if (fCost == other.fCost) return 0;
            if (fCost < other.fCost) return -1;
            else return 1;
        }
    }
}
