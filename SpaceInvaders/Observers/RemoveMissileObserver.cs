using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveMissileObserver : ColObserver
    {
        private GameObject pMissile;

        public RemoveMissileObserver()
        {
            this.pMissile = null;
        }

        public RemoveMissileObserver(RemoveMissileObserver m)
        {
            Debug.Assert(m.pMissile != null);
            this.pMissile = m.pMissile;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pMissile = (Missile)this.pSubject.pObjA;

            //Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);


            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;

                // Delay - remove object later
                // ToDo - reduce the new functions
                //AnimationSprite pAnimUFODies = new AnimationSprite(GameSprite.Name.Missile);

                //pAnimUFODies.Attach(Image.Name.MissileBombDies);

                //TimerMan.Add(TimerEvent.Name.SquidAnimation, pAnimUFODies, 0.00f, false);

                RemoveMissileObserver pObserver = new RemoveMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pMissile.Remove();
        }
    }
}