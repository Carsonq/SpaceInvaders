using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveSprite : Command
    {
        private GameObject pGameObj;

        public RemoveSprite(GameObject pGO)
        {
            this.pGameObj = pGO;
            Debug.Assert(this.pGameObj != null);
        }

        public override void Execute(float deltaTime, bool repeat)
        {
            this.pGameObj.Remove();
        }

        public override void UpdateRange(int delta) { }
    }
}