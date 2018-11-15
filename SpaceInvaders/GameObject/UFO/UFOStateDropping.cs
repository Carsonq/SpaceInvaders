using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOStateDropping : UFOState
    {
        public override void Handle(UFO pUFO)
        {
        }

        public override void Move(UFO pUFO)
        {
            pUFO.SetXY(pUFO.GetX() - 2.5f, pUFO.GetY());
        }

        public override void Drop(UFO pUFO)
        {
        }

        public override void StopSound(UFO pUFO)
        {
            pUFO.GetSound().Stop();
        }
    }
}