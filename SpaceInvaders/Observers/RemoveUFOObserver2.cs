using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveUFOObserver2 : ColObserver
    {
        private GameObject pUFO;

        public RemoveUFOObserver2()
        {
            this.pUFO = null;
        }

        public RemoveUFOObserver2(RemoveUFOObserver2 u)
        {
            Debug.Assert(u != null);
            this.pUFO = u.pUFO;
        }

        public override void Notify()
        {
            this.pUFO = (UFO)this.pSubject.pObjB;
            Debug.Assert(this.pUFO != null);

            if (pUFO.bMarkForDeath == false)
            {
                pUFO.bMarkForDeath = true;
                //   Delay
                RemoveUFOObserver2 pObserver = new RemoveUFOObserver2(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            ((UFO)this.pUFO).StopSound();
            SndObserver down = new SndObserver(SceneStateGame.sndEngine, SndObserver.Name.UFOFlyLow);
            down.PlaySound();

            this.pUFO.Remove();
        }
    }
}