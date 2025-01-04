using System.Collections.Generic;
using Stride.Engine;
using Stride.Core.Mathematics;
using Stride.Input;

namespace TurnBasedStrategyCourse
{
    public class Grid : SyncScript
    {
        private int width;
        private int length;
        private Unit unit;


        public Entity TestModel;
        private GridSystemVisual gridSystem2;

        public override void Start()
        {
            //GridSystem gridSystem = new GridSystem(10, 10, 2f);
            unit = TestModel.Get<Unit>();

        }

        public override void Update()
        {
            if (Input.IsKeyDown(Keys.P))
            {
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetPosition());

                GridPosition startGridPosition = new GridPosition(0, 0);

                List<GridPosition> gridPositionList =Pathfinding.Instance.FindPath(startGridPosition, mouseGridPosition);
                
                for (int i = 0; i < gridPositionList.Count - 1; i++)
                {
                    DebugText.Print($"{LevelGrid.Instance.GetWorldPosition(gridPositionList[i])},{LevelGrid.Instance.GetWorldPosition(gridPositionList[i + 1])}", new Int2(100, 200));
                }
            }
        }
    }
}
