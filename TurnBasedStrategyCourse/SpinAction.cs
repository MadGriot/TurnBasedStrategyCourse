using Stride.Core.Mathematics;
using Stride.Engine;
using System;
using System.Collections.Generic;

namespace TurnBasedStrategyCourse
{
    public class SpinAction : BaseAction
    {
        private float totalSpinAmount;

        public override void Start()
        {
            Name = "Spin";
        }

        public override void Update()
        {
            if (!isActive)
            {
                return;
            }
            float spinAddAmount = 360f * (float)Game.UpdateTime.Elapsed.TotalSeconds;
            Entity.Transform.RotationEulerXYZ += new Vector3(0, spinAddAmount, 0);
            totalSpinAmount += spinAddAmount;
            if (totalSpinAmount >= 360f)
            {
                ActionComplete();

            }
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            ActionStart(onActionComplete);
            totalSpinAmount = 0f;
        }

        public override List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.Get<Unit>().gridPosition;

            return new List<GridPosition> { unitGridPosition };
        }

        public override int GetActionPointsCost()
        {
            return 2;
        }
    }
}