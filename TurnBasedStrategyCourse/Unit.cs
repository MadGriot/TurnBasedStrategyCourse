using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace TurnBasedStrategyCourse
{
    public class Unit : SyncScript
    {
        public Entity character;
        public GridPosition gridPosition { get; private set; }
        public MoveAction moveAction { get; private set; }

        public SpinAction spinAction { get; private set; }

        public override void Start()
        {
            moveAction = Entity.Get<MoveAction>();
            spinAction = Entity.Get<SpinAction>();
            gridPosition = LevelGrid.Instance.GetGridPosition(character.Transform.Position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }
        public override void Update()
        {
            DebugText.Print($"{character.Transform.Position}", new Int2(200, 300));
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(character.Transform.Position);
            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }
    }
}
