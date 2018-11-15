using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ChangeScene : Command
    {
        private CreateShipObserver pScene;

        public ChangeScene(CreateShipObserver pScene)
        {
            this.pScene = pScene;
            Debug.Assert(this.pScene != null);
        }

        public override void Execute(float deltaTime, bool repeat)
        {
            DelayedObjectMan.Attach(this.pScene);
        }

        public override void UpdateRange(int delta) { }
    }
}