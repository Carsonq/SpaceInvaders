using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileDies : ExplosionCategory
    {

        public MissileDies(GameObject.Name gOName, GameSprite.Name gSeName, float x, float y)
            : base(gOName, gSeName)
        {
            this.SetXY(x, y);
        }

        override public void Update()
        {
            base.Update();
        }
    }
}