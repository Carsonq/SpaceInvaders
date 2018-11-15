using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ExplosionGroup : Composite
    {
        public ExplosionGroup(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            //this.poColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);

            base.Update();
        }
    }
}
