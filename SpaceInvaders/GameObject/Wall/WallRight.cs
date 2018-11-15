using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallRight : WallCategory
    {
        public WallRight(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, WallCategory.Type.Right)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        ~WallRight()
        {

        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallRight(this);
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        // Alien vs Wall may get error when dropping a bomb without this.
        public override void VisitBomb(Bomb b) { }

        public override void VisitUFO(UFO u) { }

        public override void VisitGroup(AlienGroup a)
        {
            // AlienGrid vs WallRight
            //       Debug.WriteLine("collide: {0} with {1}", this, a);
            //Debug.WriteLine("   --->DONE<----");

            //GameObject.ChangeDirection(-1);
            //a.SetDelta(-1.0f);
            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }
    }
}