using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFO : UFOCategory
    {
        private SndObserver pSnd;
        private UFOState state;
        private IrrKlang.ISound pISnd;

        public UFO(GameObject.Name gOName, GameSprite.Name gSeName, float x, float y, SndObserver pSndObs)
            : base(gOName, gSeName, UFOCategory.Type.UFO)
        {
            this.SetXY(x, y);
            this.pSnd = pSndObs;
            this.state = new UFOStateEnd();
        }
        
        public override void Accept(ColVisitor other)
        {    
            other.VisitUFO(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Missile vs UFO
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        override public void Update()
        {
            //this.PlaySound();
            this.state.Move(this);
            this.state.Drop(this);
            base.Update();
        }

        public void SetState(UFOMan.State inState)
        {
            this.state = UFOMan.GetState(inState);
        }

        public void PlaySound()
        {
            this.pISnd = this.pSnd.PlaySound();
        }

        public void StopSound()
        {
            this.state.StopSound(this);
        }

        public IrrKlang.ISound GetSound()
        {
            return this.pISnd;
        }
    }
}
