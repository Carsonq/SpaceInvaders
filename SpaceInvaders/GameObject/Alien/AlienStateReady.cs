using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienStateReady : AlienState
    {
        public override void Handle(AlienCategory pAlien)
        {
            pAlien.SetState(AlienMan.State.BombFlying);
        }

        public override void LoadBomb(AlienCategory pAlien)
        {
        }

        public override void DropBomb(AlienCategory pAlien)
        {
            Bomb pBomb = AlienMan.ActivateBomb(pAlien);

            pBomb.SetPos(pAlien.x + 10 * AlienGroup.GetXDirection(), pAlien.y-15);

            // switch states
            this.Handle(pAlien);
        }
    }
}