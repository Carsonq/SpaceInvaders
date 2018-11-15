using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class StartGameObserver : InputObserver
    {
        int mode;
        public StartGameObserver(int m)
        {
            this.mode = m;
        }

        public override void Notify()
        {
            Scene pScene = SceneMan.GetScene();
            pScene.Unload();
            SceneStateGame.SetPlayMode(this.mode);
        }
    }
}