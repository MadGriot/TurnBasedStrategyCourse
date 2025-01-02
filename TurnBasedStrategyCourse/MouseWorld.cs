using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;
using Stride.Graphics;

namespace TurnBasedStrategyCourse
{
    public class MouseWorld : SyncScript
    {
        //public Entity unitToMove;

        public CollisionFilterGroupFlags CollideWith;
        public bool CollideWithTriggers;
        internal CameraComponent camera;
        private Simulation simulation;
        private Vector3 targetPosition;
        public static MouseWorld Instance;

        public override void Start()
        {
            Instance = this;
            camera = Entity.Get<CameraComponent>();
            simulation = this.GetSimulation();
        }

        public override void Update()
        {

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(GetPosition());

            }

            DebugText.Print($"{targetPosition.ToString()}", new Int2(599, 200));
        }


        public Vector3 GetPosition()
        {
            Texture backbuffer = GraphicsDevice.Presenter.BackBuffer;
            Viewport viewport = new Viewport(0, 0, backbuffer.Width, backbuffer.Height);

            Vector3 nearPosition = viewport.Unproject(new Vector3(Input.AbsoluteMousePosition, 0),
                camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);

            Vector3 farPosition = viewport.Unproject(new Vector3(Input.AbsoluteMousePosition, 1.0f),
                camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);

            HitResult hitResult = simulation.Raycast(nearPosition, farPosition);

            return hitResult.Point;
        }

        public HitResult HandleUnitSelection()
        {
            Texture backbuffer = GraphicsDevice.Presenter.BackBuffer;
            Viewport viewport = new Viewport(0, 0, backbuffer.Width, backbuffer.Height);

            Vector3 nearPosition = viewport.Unproject(new Vector3(Input.AbsoluteMousePosition, 0),
                camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);

            Vector3 farPosition = viewport.Unproject(new Vector3(Input.AbsoluteMousePosition, 1.0f),
                camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);


            simulation.Raycast(nearPosition, farPosition,
                out HitResult hitResult, CollisionFilterGroups.CustomFilter1, CollideWith, CollideWithTriggers);

            return hitResult;

        }
    }
}
