using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class UFOState
    {
        // state()
        public abstract void Handle(UFO pUFO);

        // strategy()
        public abstract void Move(UFO pUFO);

        public abstract void Drop(UFO pUFO);

        public abstract void StopSound(UFO pUFO);
    }
}