using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Events;

namespace TurnBasedStrategyCourse
{
    public class ActionButtonUI : SyncScript
    {

        private BaseAction baseAction;
 
        public override void Start()
        {

        }
        public override void Update()
        {
            // Do stuff every new frame
        }
        public void SetBaseAction(BaseAction baseAction, Button button)
        {
            this.baseAction = baseAction;
            button.Click += ActionButtonUI_Click;
        }

        private void ActionButtonUI_Click(object sender, RoutedEventArgs e)
        {
            UnitActionSystem.Instance.selectedAction = baseAction;
        }


    }
}
