using System;
using System.Collections.Generic;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace TurnBasedStrategyCourse
{

    public class MoveAction : BaseAction
    {
        public int maxMoveDistance = 4;
        private Vector3 targetPosition;


        public override void Start()
        {
            Name = "Move";
            base.Start();
            targetPosition = Entity.Transform.Position;
        }

        public override void Update()
        {

            if (!isActive)
            {
                return;
            }
            float stoppingDistance = .1f;
            if (Vector3.Distance(Entity.Transform.Position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = Vector3.Normalize(targetPosition - Entity.Transform.Position);
                float moveSpeed = 4f;
                characterComponent.SetVelocity(moveDirection * moveSpeed);
            }
            else
            {
                characterComponent.SetVelocity(Vector3.Zero);
                ActionComplete();

            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            ActionStart(onActionComplete);
            targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        }

        public override List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unitComponent.gridPosition;
            int accum = 0;
            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }
                    if (unitGridPosition == testGridPosition)
                    {
                        //You're already at this grid position.
                        continue;
                    }

                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        //Another unit is at this position.
                        continue;
                    }
                    DebugText.Print(testGridPosition.ToString(), new Int2(400, 50 + accum));
                    validGridPositionList.Add(testGridPosition);
                    accum += 25;
                }
            }
            return validGridPositionList;
        }
    }
}
