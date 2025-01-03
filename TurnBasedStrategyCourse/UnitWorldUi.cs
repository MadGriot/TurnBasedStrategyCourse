using System;
using System.Collections.Generic;
using System.Linq;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI.Controls;
using Stride.Graphics;
using Stride.UI;

namespace TurnBasedStrategyCourse
{
    public class UnitWorldUi : SyncScript
    {

        public Prefab healthStatus;
        private TextBlock actionPointsText;
        private Entity page;
        private int incrementer;
        private List<Unit> units;
        public override void Start()
        {
            page = healthStatus.Instantiate().First();
            CreateHeathBars();
            GridSystemVisual.Instance.UpdateGridVisual();
        }

        public override void Update()
        {
            DebugText.Print($"{units.Count}", new Int2(600, 50));
        }
        private void CreateHeathBars()
        {     
            units = LevelGrid.Instance.gridSystem.GetAllUnits();
            foreach (Unit entity in units)
            {
                page.Get<UIComponent>().Page.RootElement.FindVisualChildOfType<TextBlock>().Text = $"{entity.character.Name}";
                entity.characterSheetLogic.HPSlider =  page.Get<UIComponent>().Page.RootElement.FindVisualChildOfType<Slider>();
                entity.characterSheetLogic.HPSlider.Value = entity.characterSheetLogic.HP / entity.characterSheetLogic.maxHP;
                page.Get<UIComponent>().Page.RootElement.Margin += new Thickness(0, incrementer, 0, 0);
                Entity.Scene.Entities.Add(page);
                page = healthStatus.Instantiate().First();
                incrementer += 70;
            }
        }
        private void UpdateActionPointsText()
        {

        }
    }
}
