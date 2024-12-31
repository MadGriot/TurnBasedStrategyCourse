using System;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.UI.Controls;
using Stride.UI.Events;

namespace TurnBasedStrategyCourse
{
    public class ActionButtonUI : SyncScript
    {

        private BaseAction baseAction;
        private Button button;

        public override void Start()
        {
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedUnitChanged;
        }
        public override void Update()
        {

        }
        public void SetBaseAction(BaseAction baseAction, Button button)
        {
            this.baseAction = baseAction;
            button.Click += ActionButtonUI_Click;
            this.button = button;


        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            UpdateSelectedVisual();
        }
        private void ActionButtonUI_Click(object sender, RoutedEventArgs e)
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);

        }


        public void UpdateSelectedVisual()
        {
            BaseAction selectedBaseAction = UnitActionSystem.Instance.selectedAction;

            if (selectedBaseAction.Equals(baseAction))
            {
                button.BackgroundColor = Color.White;
            }
            else
            {
                button.BackgroundColor = Color.Black;
            }
        }

    }
}
