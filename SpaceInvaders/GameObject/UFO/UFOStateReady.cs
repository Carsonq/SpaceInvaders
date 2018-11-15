using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOStateReady : UFOState
    {
        public override void Handle(UFO pUFO)
        {
            pUFO.SetState(UFOMan.State.Flying);
        }

        public override void Move(UFO pUFO)
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            Random random = new Random(iSeed);

            int movePossibility = random.Next(0, 200);
            if (movePossibility == 5)
            {
                this.Handle(pUFO);
                pUFO.PlaySound();
            }
        }

        public override void Drop(UFO pUFO)
        {
        }

        public override void StopSound(UFO pUFO)
        {

        }
    }
}