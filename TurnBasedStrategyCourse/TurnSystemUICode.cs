using System;
using Stride.Engine;
using Stride.Core.Mathematics;
using Stride.UI.Controls;
using Stride.UI;
using Stride.UI.Events;

namespace TurnBasedStrategyCourse
{
    public class TurnSystemUICode : SyncScript
    {
        public Entity turnUI;
        public Entity turnVisual;
        private Button endTurnButton;
        private TextBlock turnNumberText;
        private TextBlock turnText;

        public override void Start()
        {
            endTurnButton = turnUI.Get<UIComponent>().Page
                .RootElement.FindVisualChildOfType<Button>("EndTurnButton");
            turnNumberText = turnUI.Get<UIComponent>().Page
                .RootElement.FindVisualChildOfType<TextBlock>("TurnNumberText");
            turnText = turnVisual.Get<UIComponent>().Page
                .RootElement.FindVisualChildOfType<TextBlock>();

            endTurnButton.Click += EndTurn_ButtonClicked;

            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            UpdateTurnText();
            UpdateTurnVisual();
            UpdateEndTurnButtonVisibility();
        }
        private void EndTurn_ButtonClicked(object sender, RoutedEventArgs e)
        {
            TurnSystem.Instance.NextTurn();
        }
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            UpdateTurnText();
            UpdateTurnVisual();
            UpdateEndTurnButtonVisibility();

        }

        private void UpdateTurnText()
        {
            turnNumberText.Text = $"Turn {TurnSystem.Instance.turnNumber}";
        }

        private void UpdateTurnVisual()
        {
            if (TurnSystem.Instance.isPlayerTurn)
            {
                turnText.TextColor = Color.White;
                turnText.Text = $"Player Turn";
            }
            else
            {
                turnText.TextColor = Color.Red;
                turnText.Text = $"Enemy Turn";
            }

        }

        private void UpdateEndTurnButtonVisibility()
        {
            if (TurnSystem.Instance.isPlayerTurn) endTurnButton.Visibility = Visibility.Visible;
            else endTurnButton.Visibility = Visibility.Hidden;
            endTurnButton.IsEnabled = TurnSystem.Instance.isPlayerTurn;
        }
        public override void Update()
        {

        }
    }
}
