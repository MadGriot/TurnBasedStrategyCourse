using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI.Controls;
using Stride.Graphics;

namespace TurnBasedStrategyCourse
{
    public class UnitWorldUi : SyncScript
    {

        public Prefab healthStatus;
        private TextBlock actionPointsText;
        public Entity entity;
        private Unit unit;
        private Entity page;

        public override void Start()
        {
            page = healthStatus.Instantiate().First();
            Entity.Scene.Entities.Add(page);
        }

        public override void Update()
        {

            
        }

        private void UpdateActionPointsText()
        {

        }
    }
}
