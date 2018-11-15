using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SelectGameObserver : InputObserver
    {
        public override void Notify()
        {
            String pScoreHigh = Int32.Parse(FontMan.Find(Font.Name.ScoreHigh).GetMessage()).ToString();
            SceneStateTract.SetScoreHigh(pScoreHigh);

            Scene pScene = SceneMan.GetScene();
            pScene.Unload();
        }
    }
}