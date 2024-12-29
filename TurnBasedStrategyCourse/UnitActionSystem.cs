using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace TurnBasedStrategyCourse
{
    public class UnitActionSystem : SyncScript
    {
        public static UnitActionSystem Instance { get; private set; }
        public Entity SelectedUnit;
        public Unit unit { get; private set; }
        public CameraComponent camera;
        private Entity clickResult;
        public GridPosition gridPosition { get; private set; }
        private bool isBusy;
        public override void Start()
        {
            
            Instance = this;
            unit = SelectedUnit.Get<Unit>();

        }

        public override void Update()
        {
            if (isBusy)
            {
                return;
            }

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                if (MouseWorld.Instance.HandleUnitSelection().Succeeded)
                {
                    SelectedUnit = MouseWorld.Instance.HandleUnitSelection().Collider.Entity;
                    unit = SelectedUnit.Get<Unit>();
                }


                if (clickResult != null)
                {
                    SelectedUnit = clickResult;
                    unit = SelectedUnit.Get<Unit>();
                }
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetPosition());

                if (unit.moveAction.IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy();
                    unit.moveAction.Move(mouseGridPosition, ClearBusy);
                }


            }

            if (Input.IsMouseButtonPressed(MouseButton.Right))
            {
                SetBusy();
                unit.spinAction.Spin(ClearBusy);
            }

            if (Input.IsMouseButtonDown(MouseButton.Right))
            {
                DebugText.Print($"{SelectedUnit.Name}", new Int2(700, 320));
                DebugText.Print($"{clickResult}", new Int2(700, 420));
            }

        }

        private void SetBusy() => isBusy = true;
        private void ClearBusy() => isBusy = false;
    }
}