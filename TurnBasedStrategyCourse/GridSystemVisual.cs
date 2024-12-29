using System.Collections.Generic;
using Stride.Engine;
using Stride.Rendering;

namespace TurnBasedStrategyCourse
{
    public class GridSystemVisual : SyncScript
    {
        public static GridSystemVisual Instance { get; private set; }
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

        public void ShowGridPositionList(List<GridPosition> gridPositionList)
        {
            foreach (GridPosition gridPosition in gridPositionList) { 

                    cells[gridPosition.x, gridPosition.z].Get<ModelComponent>().Materials.Remove(0);
                    cells[gridPosition.x, gridPosition.z].Get<ModelComponent>().Materials.Add(0, originalMaterial);

                }
         
        }

        private void UpdateGridVisual()
        {
            HideAllGridPosition();

            ShowGridPositionList(UnitActionSystem.Instance.unit.moveAction.GetValidActionGridPositionList());
        }

        public override void Update()
        {
            UpdateGridVisual();
        }
    }
}
