using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienState
    {
        // state()
        public abstract void Handle(AlienCategory pAlien);

        // strategy()
        public abstract void LoadBomb(AlienCategory pAlien);
        public abstract void DropBomb(AlienCategory pAlien);

    }
}