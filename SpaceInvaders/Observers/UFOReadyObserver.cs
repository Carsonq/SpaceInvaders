using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOReadyObserver : ColObserver
    {
        IrrKlang.ISoundEngine pSndEngine;

        public UFOReadyObserver(IrrKlang.ISoundEngine pEng)
        {
            this.pSndEngine = pEng;
        }
        public override void Notify()
        {
            //UFO pUFO = UFOMan.GetUFO();
            //pUFO.SetState(UFOMan.State.Ready);
            UFOMan.Create(new SndObserver(this.pSndEngine, SndObserver.Name.UFOFlyHigh, 0.2f, true));
        }
    }
}