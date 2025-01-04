using Stride.Engine;
using Stride.Physics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TurnBasedStrategyCourse
{
    public abstract class BaseAction : SyncScript
    {
        protected bool isActive;

        public Entity unit;
        protected Unit unitComponent;
        protected CharacterComponent characterComponent;
        protected Action onActionComplete;
        internal StrikeAction strikeAction;
        public string Name {  get; protected set; } = "Action";

        public override void Start()
        {
            characterComponent = Entity.Get<CharacterComponent>();
            strikeAction = unit.Get<StrikeAction>();
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

        public EnemyAIAction GetBestEnemyAIAction()
        {
            List<EnemyAIAction> enemyAIActions = new List<EnemyAIAction>();

            List<GridPosition> validActionGridPositions = GetValidActionGridPositionList();

            foreach (GridPosition gridPosition in validActionGridPositions)
            {
                EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
                enemyAIActions.Add(enemyAIAction);
            }
            if (enemyAIActions.Count > 0)
            {
                enemyAIActions.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
                return enemyAIActions.First();
            }
            else return null;
        }

        public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);


    }
}
