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
    public class StrikeAction : BaseAction
    {

        private enum State
        {
            Moving,
            Attacking,
            Cooloff
        }

        private State state;
        public int maxStrikeDistance { get; private set; } = 2;
        private float stateTimer;
        private Unit targetUnit;
        private bool canStrike;

        public override void Start()
        {
            Name = "Strike";
        }

        public override void Update()
        {

            if (!isActive)
            {
                return;
            }
            stateTimer -= (float)Game.UpdateTime.Total.TotalSeconds;
            switch (state)
            {
                case State.Moving:
                    if (stateTimer <= 0f)
                    {
                        state = State.Attacking;
                        stateTimer = 0.1f;
                    }
                    break;
                case State.Attacking:
                    if (stateTimer <= 0f)
                    {
                        state = State.Cooloff;
                        stateTimer = 0.5f;
                        Strike();
                        canStrike = false;
                    }
                    break;
                case State.Cooloff:
                    if (stateTimer <= 0f)
                    {
                        ActionComplete();
                    }
                    break;
            }

            Log.Debug($"{state.ToString()}");
        }

        private void Strike()
        {
            targetUnit.Damage(7f);
        }

        public override List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.Get<Unit>().gridPosition;
            for (int x = -maxStrikeDistance; x <= maxStrikeDistance; x++)
            {
                for (int z = -maxStrikeDistance; z <= maxStrikeDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    int testDistance = Math.Abs(x) + Math.Abs(z);
                    if (testDistance > maxStrikeDistance)
                    {
                        continue;
                    }
                    //validGridPositionList.Add(testGridPosition);
                    //continue;


                    if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        //Grid Position is empty, no Unit
                        continue;
                    }
                    Unit targetUnit = LevelGrid.Instance.GetAnyUnitAtGridPosition(testGridPosition);

                    if (targetUnit.isEnemy == unit.Get<Unit>().isEnemy)
                    {
                        //Both Units are friendly
                        continue;
                    }

                    validGridPositionList.Add(testGridPosition);
                }
            }
            return validGridPositionList;
        }


        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            ActionStart(onActionComplete);

            targetUnit = LevelGrid.Instance.GetAnyUnitAtGridPosition(gridPosition);
            state = State.Moving;
            stateTimer = 1f;

            canStrike = true;
        }

    }
}
