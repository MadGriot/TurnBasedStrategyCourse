using Stride.Core.Mathematics;
using Stride.Engine;
using System;

namespace TurnBasedStrategyCourse
{
    public class GridSystem
    {
        public int width { get; private set; }
        public int length { get; private set; }
        private float cellSize;
        private GridObject[,] gridObjects;
        public GridSystem(int width, int length, float cellSize)
        {
            this.width = width;
            this.length = length;
            this.cellSize = cellSize;
            gridObjects = new GridObject[width, length];

            for (int z = 0; z < length; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    gridObjects[x, z] = new GridObject(this, gridPosition);
                }
            }
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Convert.ToInt32(worldPosition.X / cellSize),
                Convert.ToInt32(worldPosition.Z / cellSize));
        }
        public void CreateDebugObjects(Entity debugObject)
        {
            for (int z = 0; z < length; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    Entity clone = debugObject.Clone();
                    clone.Transform.Position = new Vector3(x * cellSize, 0 * cellSize, z * cellSize);
                    debugObject.Scene.Entities.Add(clone);
                }
            }

        }
        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return gridObjects[gridPosition.x, gridPosition.z];
        }
        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            return gridPosition.x >= 0 && 
                gridPosition.z >= 0 && 
                gridPosition.x < width && 
                gridPosition.z < length;
        }
    }
}
