using Stride.Engine;
using Stride.Physics;
using System;

namespace TurnBasedStrategyCourse
{
    public abstract class BaseAction : SyncScript
    {
        protected bool isActive;

        public Entity unit;
        protected Unit unitComponent;
        protected CharacterComponent characterComponent;
        protected Action onActionComplete;
        public string Name {  get; protected set; } = "Action";

        public override void Start()
        {
            characterComponent = Entity.Get<CharacterComponent>();
            unitComponent = unit.Get<Unit>();

        }

        public override void Update()
        {
            // Do stuff every new frame
        }
    }
}
