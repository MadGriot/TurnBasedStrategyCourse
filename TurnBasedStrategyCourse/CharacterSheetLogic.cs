using System;
using Stride.Engine;
using Stride.UI.Controls;

namespace TurnBasedStrategyCourse
{
    public class CharacterSheetLogic : SyncScript
    {
        public event EventHandler OnUnconcious;
        public float HP = 20f;
        public float maxHP = 20f;
        public Slider HPSlider;

        public void Damage(float damageAmount)
        {

            HP -= damageAmount;
            HPSlider.Value = HP / maxHP;

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
