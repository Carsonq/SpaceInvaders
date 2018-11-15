using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BumperRight : BumperCategory
    {
        public BumperRight(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, BumperCategory.Type.BumperRight)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 0, 0);
        }

        ~BumperRight()
        {
        }

        public override void Accept(ColVisitor other)
        {         
            other.VisitBumperRight(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            //Debug.WriteLine("   --->DONE<----");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(s, this);
            pColPair.NotifyListeners();
        }
    }
}