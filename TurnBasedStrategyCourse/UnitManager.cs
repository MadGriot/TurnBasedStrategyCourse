using System;
using System.Collections.Generic;
using Stride.Engine;

namespace TurnBasedStrategyCourse
{
    public class UnitManager : SyncScript
    {
        public List<Unit> unitList { get; private set; }
        public List<Unit> friendlyUnitList { get; private set; }
        public List<Unit> enemyUnitList { get; private set; }

        public static UnitManager Instance { get; private set; }

        public override void Start()
        {
            Instance = this;
            unitList = new List<Unit>();
            friendlyUnitList = new List<Unit>();
            enemyUnitList = new List<Unit>();
            Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
            Unit.OnAnyUnitUnconcious += Unit_OnAnyUnitUnconcious;

        }

        private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
        {
            Unit unit = sender as Unit;
            unitList.Add(unit);
            if (unit.isEnemy)
                enemyUnitList.Add(unit);
            else
                friendlyUnitList.Add(unit);
        }

        private void Unit_OnAnyUnitUnconcious(object sender, EventArgs e)
        {
            Unit unit = sender as Unit;
            unitList.Remove(unit);
            if (unit.isEnemy)
                enemyUnitList.Remove(unit);
            else
                friendlyUnitList.Remove(unit);
        }
        public override void Update()
        {
            // Do stuff every new frame
        }
    }
}
