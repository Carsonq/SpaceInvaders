using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GridRemoveAlienObserver : ColObserver
    {
        GameObject pAlien;

        public GridRemoveAlienObserver()
        {
            pAlien = null;
        }

        public GridRemoveAlienObserver(GridRemoveAlienObserver m)
        {
            Debug.Assert(m.pAlien != null);
            this.pAlien = m.pAlien;
        }

        public override void Notify()
        {
            this.pAlien = (AlienCategory)this.pSubject.pObjB;

            if (pAlien.bMarkForDeath == false)
            {
                this.IncreaseHorizontalMoveRange();
                pAlien.bMarkForDeath = true;
                GridRemoveAlienObserver pObserver = new GridRemoveAlienObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        private void IncreaseHorizontalMoveRange()
        {
            Component col = ForwardIterator.GetParent(this.pAlien);

            if ((col.pNext == null || col.pPrev == null)  && this.pAlien == ForwardIterator.GetChild(col))
            {
                TimerMan.UpdateMovementRange(5);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            GameObject pA = (GameObject)this.pAlien;
            GameObject pB = (GameObject)Iterator.GetParent(pA);

            pA.Remove();

            // TODO: Need a better way... 
            if (PrivCheckParent(pB) == true)
            {
                GameObject pC = (GameObject)Iterator.GetParent(pB);
                pB.Remove();

                if (PrivCheckParent(pC) == true)
                {
                    SceneMan.ChangeSceneInternal(pC);
                }
            }
        }

        private bool PrivCheckParent(GameObject pObj)
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
