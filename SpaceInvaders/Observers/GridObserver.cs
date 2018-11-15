using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GridObserver : ColObserver
    {
        private bool flag;
        public GridObserver()
        {
            this.flag = true;
        }
        
        private void MoveDonw()
        {
            GameObject pAlienGroup = GameObjectMan.Find(GameObject.Name.AlienGroup);

            ReverseIterator pRev = new ReverseIterator(pAlienGroup);
            Component pNode = pRev.First();

            while (!pRev.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;

                float nextPosY = pGameObj.GetY() - 20;

                pGameObj.SetXY(pGameObj.GetX(), nextPosY);

                pNode = pRev.Next();
            }
        }

        public override void Notify()
        {
            WallCategory pWall = (WallCategory)this.pSubject.pObjB;
            if (pWall.GetCategoryType() == WallCategory.Type.Right && this.flag == true)
            {
                this.flag = false;
                MoveDonw();
                AlienGroup.ChangeDirection(-1);
            }
            else if (pWall.GetCategoryType() == WallCategory.Type.Left && this.flag == false)
            {
                this.flag = true;
                MoveDonw();
                AlienGroup.ChangeDirection(1);
            }
        }
    }
}