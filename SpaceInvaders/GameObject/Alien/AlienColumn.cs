using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienColumn : Composite
    {
        public float size;

        public AlienColumn(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.size = 0;
            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        override public void Update()
        {
            //Component pComponent = ForwardIterator.GetSibling(this);
            //pComponent = ForwardIterator.GetChild(pComponent);

            base.BaseUpdateBoundingBox(this);

            base.Update();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1} {2}", m.GetName(), this.GetName(), this.GetHashCode());

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdColumn
            // Call the appropriate collision reaction            
            other.VisitColumn(this);
        }
    }
}
