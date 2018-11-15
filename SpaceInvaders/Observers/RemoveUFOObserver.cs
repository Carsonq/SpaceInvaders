using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveUFOObserver : ColObserver
    {
        private GameObject pUFO;

        public RemoveUFOObserver()
        {
            this.pUFO = null;
        }

        public RemoveUFOObserver(RemoveUFOObserver u)
        {
            Debug.Assert(u != null);
            this.pUFO = u.pUFO;
        }

        public override void Notify()
        {
            this.pUFO = (UFO)this.pSubject.pObjA;
            Debug.Assert(this.pUFO != null);

            if (pUFO.bMarkForDeath == false)
            {
                pUFO.bMarkForDeath = true;
                //   Delay
                //AnimationSprite pAnimUFODies = new AnimationSprite(GameSprite.Name.UFO);

                //pAnimUFODies.Attach(Image.Name.UFODies);
                //pAnimUFODies.Attach(Image.Name.UFO);

                //TimerMan.Add(TimerEvent.Name.SquidAnimation, pAnimUFODies, 0.01f, false);

                RemoveUFOObserver pObserver = new RemoveUFOObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            ((UFO)this.pUFO).StopSound();

            this.pUFO.Remove();
        }
    }
}