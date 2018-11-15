using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBomb2Observer : ColObserver
    {
        private GameObject pBomb;

        public RemoveBomb2Observer()
        {
            this.pBomb = null;
        }

        public RemoveBomb2Observer(RemoveBomb2Observer b)
        {
            Debug.Assert(b != null);
            this.pBomb = b.pBomb;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pBomb = (Bomb)this.pSubject.pObjB;
            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBomb2Observer pObserver = new RemoveBomb2Observer(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            this.pBomb.Remove();
            ////  if this brick removed the last child in the column, then remove column
            //// Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            //GameObject pA = (GameObject)this.pBomb;
            //GameObject pB = (GameObject)Iterator.GetParent(pA);

            //pA.Remove();

            //// TODO: Need a better way... 
            //if (privCheckParent(pB) == true)
            //{
            //    GameObject pC = (GameObject)Iterator.GetParent(pB);
            //    pB.Remove();

            //    if (privCheckParent(pC) == true)
            //    {
            //        //        pC.Remove();
            //    }

            //}
        }

        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }
    }
}