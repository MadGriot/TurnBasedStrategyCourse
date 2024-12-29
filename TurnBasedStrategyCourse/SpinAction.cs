using Stride.Core.Mathematics;
using Stride.Engine;
using System;

namespace TurnBasedStrategyCourse
{
    public class SpinAction : BaseAction
    {
        private float totalSpinAmount;


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
                isActive = false;
                onActionComplete();
            }
        }

        public void Spin(Action onActionComplete)
        {
            this.onActionComplete = onActionComplete;
            isActive = true;
            totalSpinAmount = 0f;
        }
    }
}
