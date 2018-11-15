using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SceneState
    {
        // state()
        public abstract void Handle();

        // strategy()
        public abstract void LoadContent();
        public abstract void Update(float systemTime);
        public abstract void Draw();
        public abstract void Unload();
    }
}