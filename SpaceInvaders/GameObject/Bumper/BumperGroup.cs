using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BumperGroup : Composite
    {
        public BumperGroup(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        ~BumperGroup()
        {

        }

        public override void Accept(ColVisitor other)
        {        
            other.VisitBumperGroup(this);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }

        public override void VisitShip(Ship s)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }
    }
}