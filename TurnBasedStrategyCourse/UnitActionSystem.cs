using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System;

namespace TurnBasedStrategyCourse
{
    public class UnitActionSystem : SyncScript
    {
        public static UnitActionSystem Instance { get; private set; }
        public Entity SelectedUnit;
        public event EventHandler OnSelectedUnitChanged;

        public Unit unit { get; private set; }
        public CameraComponent camera;
        public GridPosition gridPosition { get; private set; }
        private bool isBusy;

        public BaseAction selectedAction { get; set; }
        public override void Start()
        {
            
            Instance = this;
            unit = SelectedUnit.Get<Unit>();
            selectedAction = SelectedUnit.Get<MoveAction>();

        }

        public override void Update()
        {
            DebugText.Print($"{SelectedUnit.Transform.Position}", new Int2(200, 300));
            DebugText.Print($"Action Count: {SelectedUnit.Get<Unit>().baseActionList.Count}", new Int2(200, 500));
            if (isBusy)
            {
                return;
            }

            if (TryHandleUnitSelection())
            {
                return;
            }

            HandleSelectedAction();

            if (Input.IsMouseButtonDown(MouseButton.Right))
            {
                DebugText.Print($"{SelectedUnit.Name}", new Int2(700, 320));
            }

        }
        private void HandleSelectedAction()
        {
            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetPosition());
                switch (selectedAction)
                {
                    case MoveAction moveAction:
                        if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                        {
                            SetBusy();
                            moveAction.Move(mouseGridPosition, ClearBusy);
                        }
                        break;
                    case SpinAction spinAction:
                            SetBusy();
                            unit.spinAction.Spin(ClearBusy);
                        spinAction.Spin(ClearBusy);
                        break;
                }
            }
        }

        private bool TryHandleUnitSelection()
        {
            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                if (MouseWorld.Instance.HandleUnitSelection().Succeeded)
                {
                    SelectedUnit = MouseWorld.Instance.HandleUnitSelection().Collider.Entity;
                    unit = SelectedUnit.Get<Unit>();
                    selectedAction = unit.moveAction;
                    
                    OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
            return false;
        }
        private void SetBusy() => isBusy = true;
        private void ClearBusy() => isBusy = false;
    }
}