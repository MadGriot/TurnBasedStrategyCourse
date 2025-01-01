using System;
using Stride.Engine;
using Stride.UI.Controls;
using Stride.UI;
using Stride.UI.Events;

namespace TurnBasedStrategyCourse
{
    public class TurnSystemUICode : SyncScript
    {
        public Entity turnUI; 
        private Button endTurnButton;
        private TextBlock turnNumberText;

        public override void Start()
        {
            endTurnButton = turnUI.Get<UIComponent>().Page
                .RootElement.FindVisualChildOfType<Button>("EndTurnButton");
            turnNumberText = turnUI.Get<UIComponent>().Page
                .RootElement.FindVisualChildOfType<TextBlock>("TurnNumberText");

            endTurnButton.Click += EndTurn_ButtonClicked;

            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            UpdateTurnText();
        }
        private void EndTurn_ButtonClicked(object sender, RoutedEventArgs e)
        {
            TurnSystem.Instance.NextTurn();
        }
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            UpdateTurnText();
        }

        private void UpdateTurnText()
        {
            turnNumberText.Text = $"Turn {TurnSystem.Instance.turnNumber}";
        }
        public override void Update()
        {

        }
    }
}
