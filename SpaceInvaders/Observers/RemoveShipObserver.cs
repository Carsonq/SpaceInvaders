using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShipObserver : ColObserver
    {
        private GameObject pShip;

        public RemoveShipObserver()
        {
            this.pShip = null;
        }

        public RemoveShipObserver(RemoveShipObserver b)
        {
            Debug.Assert(b != null);
            this.pShip = b.pShip;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pShip = (Ship)this.pSubject.pObjB;
            Debug.Assert(this.pShip != null);

            if (pShip.bMarkForDeath == false)
            {
                pShip.bMarkForDeath = true;
                //   Delay
                RemoveShipObserver pObserver = new RemoveShipObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            this.pShip.Remove();
        }
    }
}