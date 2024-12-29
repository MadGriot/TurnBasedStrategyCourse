
using Stride.Engine;
using System.Collections.Generic;

namespace TurnBasedStrategyCourse
{
    public class GridObject
    {
        private GridSystem gridSystem;
        private GridPosition gridPosition;
        public List<Unit> units {  get; set; }

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;
            units = new List<Unit>();
        }

        public override string ToString()
        {
            string unitString = "";
            foreach (Unit unit in units)
            {
                unitString += unit + "\n";
            }
            return gridPosition.ToString() + "\n" + unitString;
        }

        public bool HasAnyUnit()
        {
            return units.Count > 0;
        }
    }
}
