using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOStateFlying : UFOState
    {
        public override void Handle(UFO pUFO)
        {
            pUFO.SetState(UFOMan.State.Dropping);
        }

        public override void Move(UFO pUFO)
        {
            pUFO.SetXY(pUFO.GetX() - 2.5f, pUFO.GetY());
        }

        public override void Drop(UFO pUFO)
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            Random random = new Random(iSeed);

            int movePossibility = random.Next(0, 50);
            if (movePossibility == 5)
            {
                Bomb pBomb = UFOMan.ActivateBomb(pUFO);

                pBomb.SetPos(pUFO.x, pUFO.y - 15);
                pBomb.SetActive(true);
                //pUFO.PlaySound();

                this.Handle(pUFO);
            }
        }

        public override void StopSound(UFO pUFO)
        {
            pUFO.GetSound().Stop();
        }
    }
}