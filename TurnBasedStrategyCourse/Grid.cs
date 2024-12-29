using Stride.Engine;
using Stride.Input;

namespace TurnBasedStrategyCourse
{
    public class Grid : SyncScript
    {
        private int width;
        private int length;
        private Unit unit;


        public Entity TestModel;

        public override void Start()
        {
            GridSystem gridSystem = new GridSystem(10, 10, 2f);
            unit = TestModel.Get<Unit>();

        }

        public override void Update()
        {

        }
    }
}
