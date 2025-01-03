using System;
using Stride.Engine;
using Stride.Input;

namespace TurnBasedStrategyCourse
{
    public class EnemyAI : SyncScript
    {

        private enum State
        {
            WaitingForEnemyTurn,
            TakingTurn,
            Busy,
        }

        private State state;
        private float timer;

        public override void Start()
        {
            state = State.WaitingForEnemyTurn;
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        }

        public override void Update()
        {
            if (TurnSystem.Instance.isPlayerTurn)
            {
                return;
            }

            switch (state)
            {
                case State.WaitingForEnemyTurn:
                    break;
                case State.TakingTurn:
                    timer -= (float)Game.UpdateTime.Elapsed.TotalSeconds;

                    if (timer <= 0f)
                    {
                        if (TryTakeEnemyAIAction(SetStateTakingTurn))
                            state = State.Busy;
                        else
                            TurnSystem.Instance.NextTurn();

                    }
                    break;
                case State.Busy:
                    break;
            }

        }

        private void SetStateTakingTurn()
        {
            timer = 0.5f;
            state = State.TakingTurn;
        }
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            if (!TurnSystem.Instance.isPlayerTurn)
            {
                state = State.TakingTurn;
                timer = 3f;
            }
        }

        private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete)
        {
            foreach (Unit enemyUnit in UnitManager.Instance.enemyUnitList)
            {
                if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete))
                    return true;
            }
            return false;
        }

        private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
        {
            SpinAction spinAction = enemyUnit.spinAction;

                GridPosition actionGridPosition = enemyUnit.gridPosition;

            if (spinAction.IsValidActionGridPosition(actionGridPosition))
            {
                if (enemyUnit.TrySpendActionPoints(spinAction))
                {
                    spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
                    return true;
                }
            }
            return false;
        }
    }

}
