using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ToggleShieldObserver : InputObserver
    {
        public override void Notify()
        {
            // Debug.WriteLine("Toggle Observer");
            SpriteBatchMan.ToggleShield();
        }
    }
}