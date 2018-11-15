using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveObserver : ColObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();
            if (this.pSubject.pObjB.GetName() == GameObject.Name.BumperRight)
            {
                pShip.SetMoveState(ShipMan.State.MoveLeft);
            }
            else if (this.pSubject.pObjB.GetName() == GameObject.Name.BumperLeft)
            {
                pShip.SetMoveState(ShipMan.State.MoveRight);
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}
