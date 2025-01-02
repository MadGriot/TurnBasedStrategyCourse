using System;
using Stride.Engine;

namespace TurnBasedStrategyCourse
{
    public class EnemyAI : SyncScript
    {
        private float timer;

        public override void Start()
        {
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        }

        public override void Update()
        {
            if (TurnSystem.Instance.isPlayerTurn)
            {
                return;
            }

            timer -= (float)Game.UpdateTime.Elapsed.TotalSeconds;

            if (timer <= 0f)
            {
                TurnSystem.Instance.NextTurn();
            }
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            timer = 3f;
        }
    }
}
