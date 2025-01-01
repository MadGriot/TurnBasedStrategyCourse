using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TurnBasedStrategyCourse
{
    public class Unit : SyncScript
    {
        private const int ACTION_POINTS_MAX = 3;

        public static event EventHandler OnAnyActionPointsChanged;

        public Entity character;
        public GridPosition gridPosition { get; private set; }
        public MoveAction moveAction { get; private set; }

        public SpinAction spinAction { get; private set; }
        public List<BaseAction> baseActionList { get; private set; } = new List<BaseAction>();
        internal int actionPoints = ACTION_POINTS_MAX;

        public override void Start()
        {
            moveAction = character.Get<MoveAction>();
            spinAction = character.Get<SpinAction>();
            baseActionList = character.GetAll<BaseAction>().ToList();
            gridPosition = LevelGrid.Instance.GetGridPosition(character.Transform.Position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
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

        private void SpendActionPoints(int amount)
        {
            actionPoints -= amount;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            actionPoints = ACTION_POINTS_MAX;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}
