using System;
using Stride.Engine;

namespace TurnBasedStrategyCourse
{
    public class CharacterSheetLogic : SyncScript
    {
        public event EventHandler OnUnconcious;
        public int HP = 20;

        public void Damage(int damageAmount)
        {

            HP -= damageAmount;

            if (HP < 0) KnockUnconcious();
        }
        public override void Start()
        {
            // Initialization of the script.
        }

        public override void Update()
        {
            // Do stuff every new frame
        }

        private void KnockUnconcious()
        {
            OnUnconcious?.Invoke(this, EventArgs.Empty);
        }
    }
}
