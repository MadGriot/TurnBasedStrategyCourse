using System;
using System.Collections.Generic;
using Stride.Engine;
using Stride.Rendering;

namespace TurnBasedStrategyCourse
{
    public class GridSystemVisual : SyncScript
    {
        public enum GridVisualType
        {
            White,
            Blue,
            Red,
            RedSoft,
        }
        public static GridSystemVisual Instance { get; private set; }
        public List<Material> gridVisualTypeMaterialList = new List<Material>();
        public Prefab gridSystemVisualPrefab;
        private Entity cell;
        private Entity[,] cells;
        public Material blankMaterial;
        public Material originalMaterial;

        public override void Start()
        {
            blankMaterial = Content.Load<Material>("Materials/BlankMaterial");
            originalMaterial = Content.Load<Material>("Materials/PolishedStone Transparent");
            cells = new Entity[LevelGrid.Instance.GetWidth(),LevelGrid.Instance.GetLength()];
            gridSystemVisualPrefab = Content.Load<Prefab>("Grid");
            for (int z = 0; z < LevelGrid.Instance.GetLength(); z++)
            {
                for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    List<Entity> gridInstance = gridSystemVisualPrefab.Instantiate();
                    foreach (Entity cell in gridInstance)
                    {
                        this.cell = cell;
                        cells[x,z] = cell;
                        Entity.Scene.Entities.Add(cell);
                        cell.Transform.Position = LevelGrid.Instance.GetWorldPosition(gridPosition);


                    }
                }
            }
            Instance = this;

            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            LevelGrid.Instance.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;


        }

        public void HideAllGridPosition()
        {
            for (int z = 0; z < LevelGrid.Instance.GetLength(); z++)
            {
                for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
                {

                    cells[x, z].Get<ModelComponent>().Materials.Remove(0);
                    cells[x, z].Get<ModelComponent>().Materials.Add(0, blankMaterial);

                }
            }
        }

        private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
        {
            List<GridPosition> gridPositionList = new List<GridPosition>();
            for (int z = -range; z <= range; z++)
            {
                for (int x = -range; x <= range; x ++)
                {
                    GridPosition testGridPosition = gridPosition + new GridPosition(x, z);
                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }
                    int testDistance = Math.Abs(x) + Math.Abs(z);
                    if (testDistance > range)
                    {
                        continue;
                    }

                    gridPositionList.Add(testGridPosition);
                }
            }

            ShowGridPositionList(gridPositionList, gridVisualType);
        }

        public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
        {
            foreach (GridPosition gridPosition in gridPositionList) {

                Material material = GetGridVisualTypeMaterial(gridVisualType);
                cells[gridPosition.x, gridPosition.z].Get<ModelComponent>().Materials.Remove(0);
                cells[gridPosition.x, gridPosition.z].Get<ModelComponent>().Materials.Add(0, material);

                }
         
        }

        public void UpdateGridVisual()
        {
            HideAllGridPosition();
            Entity selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            BaseAction selectedAction = UnitActionSystem.Instance.selectedAction;

            GridVisualType gridVisualType;
            switch (selectedAction)
            {
                default:
                case MoveAction moveAction:
                    gridVisualType = GridVisualType.White;
                    break;
                case SpinAction spinAction:
                    gridVisualType = GridVisualType.Blue;
                    break;
                case StrikeAction strikeAction:
                    gridVisualType = GridVisualType.Red;

                    ShowGridPositionRange(selectedUnit.Get<Unit>().gridPosition,
                        strikeAction.maxStrikeDistance, GridVisualType.RedSoft);
                    break;
            }
            ShowGridPositionList(UnitActionSystem.Instance.selectedAction
                .GetValidActionGridPositionList(), gridVisualType);
        }

        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
        {
            UpdateGridVisual();
        }
        private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, EventArgs e)
        {
            UpdateGridVisual();

        }

        private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
        {
            for (int i = 0; i < gridVisualTypeMaterialList.Count; i ++)
            {
                if (i == (int)gridVisualType)
                    return gridVisualTypeMaterialList[i];
            }
            return null;
        }
        public override void Update()
        {

        }
    }
}
