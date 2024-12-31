using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System.Collections.Generic;
using System.Linq;

namespace TurnBasedStrategyCourse
{
    public class Unit : SyncScript
    {
        public Entity character;
        public GridPosition gridPosition { get; private set; }
        public MoveAction moveAction { get; private set; }

        public SpinAction spinAction { get; private set; }
        public List<BaseAction> baseActionList { get; private set; } = new List<BaseAction>();
        internal int actionPoints = 3;

        public override void Start()
        {
            moveAction = character.Get<MoveAction>();
            spinAction = character.Get<SpinAction>();
            baseActionList = character.GetAll<BaseAction>().ToList();
            gridPosition = LevelGrid.Instance.GetGridPosition(character.Transform.Position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }
        public override void Update()
        {

            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(character.Transform.Position);
            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }

        private void AddActions(BaseAction action)
        {
            if (action != null)
            {
                baseActionList.Add(action);
            }
        }
        public bool TrySpendActionPoints(BaseAction baseAction)
        {
            if  (CanSpendActionPoints(baseAction))
            {
                SpendActionPoints(baseAction.GetActionPointsCost());
                return true;
            }
             else
            {
                return false;
            }

        }
        public bool CanSpendActionPoints(BaseAction baseAction) =>
            actionPoints >= baseAction.GetActionPointsCost();

        private void SpendActionPoints(int amount) => actionPoints -= amount;
    }
}
