using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFORoot : Composite
    {
        //float delta;

        public UFORoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            //this.delta = -6.0f;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        ~UFORoot()
        {
        }

        public override void Accept(ColVisitor other)
        {       
            other.VisitUFORoot(this);
        }

        public override void Update()
        {
            //ForwardIterator pFor = new ForwardIterator(this);

            //Component pNode = pFor.First();
            //while (!pFor.IsDone())
            //{
            //    GameObject pGameObj = (GameObject)pNode;
            //    pGameObj.x += this.delta;

            //    pNode = pFor.Next();
            //}

            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BombRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Bomb vs ShieldRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public void SetShootSound(SndObserver pSnd)
        {

        }
    }
}