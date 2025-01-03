using System;
using System.Collections.Generic;
using System.Linq;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace TurnBasedStrategyCourse
{
    public class UnitActionSystemUI : SyncScript
    {
        public Prefab actionButtonPrefab;
        Entity instance;
        Entity selectedUnit;
        private float incrementer = 0;
        public Entity actionPointUI;
        private List<Entity> ButtonContainer;
        private UIPage page;
        private BaseAction baseAction;

        private List<ActionButtonUI> actionButtons;
        public override void Start()
        {
            selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            baseAction = selectedUnit.Get<Unit>().moveAction;
            ButtonContainer = new List<Entity>();
            actionButtons = new List<ActionButtonUI>();
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
            CreateUnitActionButtons();
            UpdateActionPoints();
        }



        private void CreateUnitActionButtons()
        {
            actionButtons.Clear();
            selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            foreach (BaseAction baseAction in selectedUnit.Get<Unit>().baseActionList)
            {
                this.baseAction = baseAction;
                if (baseAction != null)
                {
                    instance = actionButtonPrefab.Instantiate().First();
                    ButtonContainer.Add(instance);
                    page = instance.Get<UIComponent>().Page;
                    page.RootElement.FindVisualChildOfType<TextBlock>().Text = baseAction.Name;
                    page.RootElement.Margin += new Thickness(0, incrementer, 0, 0);

                    instance.Get<ActionButtonUI>().SetBaseAction(baseAction, page.RootElement.FindVisualChildOfType<Button>());
                    Entity.Scene.Entities.Add(instance);

                    actionButtons.Add(instance.Get<ActionButtonUI>());
                    instance.Get<ActionButtonUI>().UpdateSelectedVisual();
                    incrementer += 70;
                }
            }
        }

        private void UpdateSelectedVisual()
        {
            foreach (ActionButtonUI button in actionButtons)
            {
                button.UpdateSelectedVisual();
            }
        }
        private void RemoveUnitActionButtons()
        {
            foreach (Entity entity in ButtonContainer)
            {
                Entity.Scene.Entities.Remove(entity);
                entity.Dispose();
            }
            incrementer = 0;
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            RemoveUnitActionButtons();
            CreateUnitActionButtons();
            UpdateActionPoints();

        }

        private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
        {
            UpdateActionPoints();
        }

        private void UpdateActionPoints()
        {
            actionPointUI.Get<UIComponent>()
                .Page.RootElement.FindVisualChildOfType<TextBlock>()
                .Text = $"Action Points: {UnitActionSystem.Instance.SelectedUnit.Get<Unit>().actionPoints}";
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            UpdateActionPoints();
        }

        private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
        {
            UpdateActionPoints();
        }
        public override void Update()
        {
        }
    }
}
