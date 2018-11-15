using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ToggleObserver : InputObserver
    {
        public override void Notify()
        {
            // Debug.WriteLine("Toggle Observer");
            SpriteBatchMan.ToggleColBox();
        }
    }
}