using System;
using System.Collections.Generic;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Events;

namespace TurnBasedStrategyCourse
{
    public class UnitActionSystemUI : SyncScript
    {
        public Prefab actionButtonPrefab;
        List<Entity> instance;
        Entity selectedUnit;
        private float incrementer = 0;
        private List<List<Entity>> ButtonContainer;
        private UIPage page;
        private BaseAction baseAction;
        public override void Start()
        {
            selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            baseAction = selectedUnit.Get<Unit>().moveAction;
            ButtonContainer = new List<List<Entity>>();
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            CreateUnitActionButtons();
        }

        private void CreateUnitActionButtons()
        {
            selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            foreach (BaseAction baseAction in selectedUnit.Get<Unit>().baseActionList)
            {
                this.baseAction = baseAction;
                if (baseAction != null)
                {
                    instance = actionButtonPrefab.Instantiate();
                    ButtonContainer.Add(instance);
                    foreach (Entity entity in instance)
                    {
                        page = entity.Get<UIComponent>().Page;
                        page.RootElement.FindVisualChildOfType<TextBlock>().Text = baseAction.Name;
                        page.RootElement.Margin += new Thickness(0, incrementer, 0, 0);

                        entity.Get<ActionButtonUI>().SetBaseAction(baseAction, page.RootElement.FindVisualChildOfType<Button>());
                        Entity.Scene.Entities.AddRange(instance);
                        incrementer += 70;

                    }
                }
            }
        }

        private void RemoveUnitActionButtons()
        {
            foreach (var entities in ButtonContainer)
            {
                foreach (Entity entity in entities)
                {
                    Entity.Scene.Entities.Remove(entity);
                }
            }
            incrementer = 0;
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            RemoveUnitActionButtons();
            CreateUnitActionButtons();

        }

        public override void Update()
        {
        }
    }
}
