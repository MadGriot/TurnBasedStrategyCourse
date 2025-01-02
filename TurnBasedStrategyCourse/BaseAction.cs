using Stride.Engine;
using Stride.Physics;
using System;
using System.Collections.Generic;

namespace TurnBasedStrategyCourse
{
    public abstract class BaseAction : SyncScript
    {
        protected bool isActive;

        public Entity unit;
        protected Unit unitComponent;
        protected CharacterComponent characterComponent;
        protected Action onActionComplete;
        public string Name {  get; protected set; } = "Action";

        public override void Start()
        {
            characterComponent = Entity.Get<CharacterComponent>();
            unitComponent = unit.Get<Unit>();

        }

        public override void Update()
        {
            // Do stuff every new frame
        }

        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

        public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
            return validGridPositionList.Contains(gridPosition);
        }

        public abstract List<GridPosition> GetValidActionGridPositionList();

        public virtual int GetActionPointsCost() => 1;

        protected void ActionStart(Action onActionComplete)
        {
            isActive = true;
            this.onActionComplete = onActionComplete;
        }

        protected void ActionComplete()
        {
            isActive = false;
            onActionComplete();
        }
    }
}
