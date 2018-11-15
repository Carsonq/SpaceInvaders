using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Octopus : AlienCategory
    {
        private AlienState state;

        public Octopus(GameObject.Name gOName, GameSprite.Name gSeName, float x, float y)
            : base(gOName, gSeName)
        {
            this.SetXY(x, y);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an RedBird
            // Call the appropriate collision reaction            
            other.VisitOctopus(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Alien vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            // Missile vs Alien
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            //// Alien vs Missile
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.GetName(), this.GetName());

            //// Missile vs Alien
            //Debug.WriteLine("-------> Done  <--------");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        override public void Update()
        {
            base.Update();
        }

        override public void DropBomb()
        {
            this.state.DropBomb(this);
        }

        override public void SetState(AlienMan.State inState)
        {
            this.state = AlienMan.GetState(inState);
        }
    }
}
