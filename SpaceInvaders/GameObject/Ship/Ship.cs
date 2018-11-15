using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class Ship : ShipCategory
    {
        public float shipSpeed;
        private ShipState state;
        private ShipState moveState;
        private SndObserver pSnd;

        public Ship(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, SndObserver pSndObs)
            :base(name, spriteName, ShipCategory.Type.Ship)
        {
            this.x = posX;
            this.y = posY;

            this.shipSpeed = 3.0f;
            this.state = null;
            this.moveState = null;
            this.pSnd = pSndObs;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Bomb
            // Call the appropriate collision reaction
            other.VisitShip(this);
        }

        public void MoveRight()
        {
            this.moveState.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.moveState.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.state.ShootMissile(this);
        }

        public void SetState(ShipMan.State inState)
        {
            this.state = ShipMan.GetState(inState);
        }

        public void SetMoveState(ShipMan.State inState)
        {
            this.moveState = ShipMan.GetState(inState);
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb vs WallRoot
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public void PlaySound()
        {
            this.pSnd.Notify();
        }
    }
}