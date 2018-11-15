using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombObserver : ColObserver
    {
        int p;

        public BombObserver(int p)
        {
            this.p = p;
        }

        public override void Notify()
        {
            //Debug.WriteLine("BombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            GameObject placeObject;
            if (this.p == 1)
            {
                placeObject = this.pSubject.pObjA;
            }
            else
            {
                placeObject = this.pSubject.pObjB;
            }

            Bomb pBomb = (Bomb)placeObject;

            GameObject pGameObj = pBomb.getOwned();
            Debug.Assert(pGameObj != null);
            if (pGameObj.GetName() == GameObject.Name.UFO)
            {
                if (pGameObj.bMarkForDeath == false)
                {
                    UFO pUFO = (UFO)pGameObj;
                    pUFO.SetState(UFOMan.State.Flying);
                }
            }
            else
            {
                if (pGameObj.bMarkForDeath == false)
                {
                    AlienCategory pAlien = (AlienCategory)pGameObj;
                    pAlien.SetState(AlienMan.State.Ready);
                }
            }
        }
    }
}