using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace TurnBasedStrategyCourse
{
    public class Pathfinding : SyncScript
    {
        public static Pathfinding Instance {  get; private set; }
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;
        private int width;
        private int length;
        private float cellSize;
        private GridSystem<PathNode> gridSystem;

        public override void Start()
        {
            Instance = this;
            gridSystem = new GridSystem<PathNode>(10, 10, 2f,
                (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));
        }

        public override void Update()
        {
            // Do stuff every new frame
        }

        public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition)
        {
            List<PathNode> openList = new List<PathNode>();
            List<PathNode > closedList = new List<PathNode>();

            PathNode startNode = gridSystem.GetTGridObject(startGridPosition);
            PathNode endNode = gridSystem.GetTGridObject(endGridPosition);
            openList.Add(startNode);

            for (int z = 0; z < gridSystem.length; z++)
            {
                for (int x = 0; x < gridSystem.width; x++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    PathNode pathNode = gridSystem.GetTGridObject(gridPosition);

                    pathNode.gCost = int.MaxValue;
                    pathNode.hCost = 0;
                    pathNode.CalculateFCost();
                    pathNode.ResetCameFromPathNode();
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistance(startGridPosition, endGridPosition);
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                PathNode currentNode = openList.Min();

                if (currentNode == endNode)
                {
                    // Reached final node
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathNode neighborNode in GetNeighborList(currentNode))
                {
                    if (closedList.Contains(neighborNode))
                    {
                        continue;
                    }

                    int tentativeGCost = 
                        currentNode.gCost + CalculateDistance(currentNode.gridPosition, neighborNode.gridPosition);

                    if (tentativeGCost < neighborNode.gCost)
                    {
                        neighborNode.SetCameFromPathNode(currentNode);
                        neighborNode.gCost = tentativeGCost;
                        neighborNode.hCost = CalculateDistance(neighborNode.gridPosition, endGridPosition);
                        neighborNode.CalculateFCost();

                        if (!openList.Contains(neighborNode))
                        {
                            openList.Add(neighborNode);
                        }
                    }
                }
            }
            //No Path Found
            return null;
        }
        private PathNode GetNode(int x, int z)
        {
             return gridSystem.GetTGridObject(new GridPosition(x, z));
        }
        public int CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB)
        {
            GridPosition gridPositionDistance = gridPositionA - gridPositionB;
            int xDistance = Math.Abs(gridPositionDistance.x);
            int zDistance = Math.Abs(gridPositionDistance.z);
            int remaining = Math.Abs(xDistance - zDistance);
            return MOVE_DIAGONAL_COST * Math.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        private List<PathNode> GetNeighborList(PathNode currentNode)
        {
            List<PathNode> neighborList = new List<PathNode>();

            GridPosition gridPosition = currentNode.gridPosition;

            if (gridPosition.x - 1 >= 0)
            {
                // Left
                neighborList.Add(GetNode(gridPosition.x - 1, gridPosition.z));
                if (gridPosition.z - 1 >= 0)
                {
                    // Left Down
                    neighborList.Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
                }
                if (gridPosition.z + 1 < gridSystem.length)
                {
                    // Left Up
                    neighborList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
                }

            }

            if (gridPosition.x + 1 < gridSystem.width)
            {
                // Right
                neighborList.Add(GetNode(gridPosition.x + 1, gridPosition.z));
                if (gridPosition.z - 1 >= 0)
                {
                    // Right Down
                    neighborList.Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
                }
                if (gridPosition.z + 1 < gridSystem.length)
                {
                    // Right Up
                    neighborList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
                }

            }
            if (gridPosition.z - 1 >= 0)
            {
                // Down
                neighborList.Add(GetNode(gridPosition.x, gridPosition.z - 1));
            }
            if (gridPosition.z + 1 < gridSystem.length)
            {
                // Up
                neighborList.Add(GetNode(gridPosition.x, gridPosition.z + 1));
            }


            return neighborList;
        }

        private List<GridPosition> CalculatePath(PathNode endNode)
        {
            List<PathNode> pathNodeList = new List<PathNode>() { endNode };
            PathNode currentNode = endNode;
            while (currentNode.cameFromPathNode != null)
            {
                pathNodeList.Add(currentNode.cameFromPathNode);
                currentNode = currentNode.cameFromPathNode;
            }
            pathNodeList.Reverse();

            List<GridPosition> gridPositions = new List<GridPosition>();
            foreach (PathNode pathNode in pathNodeList)
            {
                gridPositions.Add(pathNode.gridPosition);
            }
            return gridPositions;
        }
    }
}
