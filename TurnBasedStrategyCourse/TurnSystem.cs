using System;
using Stride.Engine;

namespace TurnBasedStrategyCourse
{
    public class TurnSystem : SyncScript
    {
        public static TurnSystem Instance { get; private set; }

        public event EventHandler OnTurnChanged;
        internal int turnNumber = 1;
        internal bool isPlayerTurn = true;

        public void NextTurn()
        {
            turnNumber++;
            isPlayerTurn = !isPlayerTurn;
            OnTurnChanged?.Invoke(this, EventArgs.Empty);
        }

        public override void Start()
        {
            Instance = this;

            OnTurnChanged?.Invoke(this, EventArgs.Empty);
        }

        public override void Update()
        {
            // Do stuff every new frame
        }
    }
}
