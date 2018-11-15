using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateMoveLeft : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMoveState(ShipMan.State.MoveBoth);
        }


        public override void MoveRight(Ship pShip)
        {
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
            this.Handle(pShip);
        }

        public override void ShootMissile(Ship pShip)
        {

        }
    }
}