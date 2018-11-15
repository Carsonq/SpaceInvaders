using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGroup : Composite
    {
        static private int moveDirectionX = 1;
        //static private int moveDirectionY = 0;

        public static void ResetDirection()
        {
            AlienGroup.moveDirectionX = 1;
            //AlienGroup.moveDirectionY = 0;
        }

        public AlienGroup(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGroup(this);
        }

        public override void VisitWallGroup(WallGroup w)
        {
            // AlienGrid vs WallGroup
            //     Debug.WriteLine("collide: {0} with {1}", this, w);

            // WallRight vs Grid
            GameObject pGameObj = (GameObject)Iterator.GetChild(w);
            ColPair.Collide(this, pGameObj);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdGroup vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            //Debug.WriteLine("update: {0}", this);
            //Component pComponent = ForwardIterator.GetSibling(this);
            //pComponent = ForwardIterator.GetChild(pComponent);

            base.BaseUpdateBoundingBox(this);

            base.Update();
        }

        public override void Move(float speedX, float speedY)
        {

            ReverseIterator pRev = new ReverseIterator(this);
            Component pNode = pRev.First();

            while (!pRev.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                if (pGameObj.holder == Container.LEAF)
                {
                    this.DropBomb(pGameObj);
                }

                float nextPosX = pGameObj.GetX() + (speedX * AlienGroup.moveDirectionX);
                //float nextPosY = pGameObj.GetY() + (speedY * AlienGroup.moveDirectionY);
                pGameObj.SetXY(nextPosX, pGameObj.GetY());

                pNode = pRev.Next();
            }

            //AlienGroup.moveDirectionY = 0;
        }

        public void DropBomb(GameObject pGameObj)
        {
            AlienCategory pAlienceCategory = (AlienCategory)pGameObj;
            if (pGameObj.pNext == null)
            {
                byte[] buffer = Guid.NewGuid().ToByteArray();
                int iSeed = BitConverter.ToInt32(buffer, 0);
                Random random = new Random(iSeed);

                int dropPossibility = random.Next(0, 100);
                if (dropPossibility < 5)
                {
                    pAlienceCategory.DropBomb();
                }
            }
        }

        public static void ChangeDirection(int x)
        {
            AlienGroup.moveDirectionX = x;
            //AlienGroup.moveDirectionY = y;
        }

        public static int GetXDirection()
        {
            return AlienGroup.moveDirectionX;
        }
    }
}
