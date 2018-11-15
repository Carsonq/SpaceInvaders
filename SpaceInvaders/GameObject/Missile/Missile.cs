using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : MissileCategory
    {
        private bool enable;
        public float delta;

        public Missile(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.enable = false;
            this.delta = 10.0f;
        }

        public override void Update()
        {
            base.Update();
            this.y += delta;
        }

        ~Missile()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~Missile():{0}", this.GetHashCode());
#endif
        }
        //public void Hit()
        //{
        //    //this.bHit = true;
        //    //base.Update();
        //}

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }

        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Remove();
        }

        public void SetActive(bool state)
        {
            this.enable = state;
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        //public override void VisitBomb(Bomb b)
        //{
        //    ColPair pColPair = ColPairMan.GetActiveColPair();
        //    pColPair.SetCollision(b, this);
        //    pColPair.NotifyListeners();
        //}
    }
}