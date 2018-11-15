using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateMoveBoth : ShipState
    {
        public override void Handle(Ship pShip)
        {

        }


        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
        }

        public override void ShootMissile(Ship pShip)
        {

        }
    }
}