using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ExplosionCategory : Leaf
    {
        public ExplosionCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName) { }

        public override void Accept(ColVisitor other)
        {
            throw new NotImplementedException();
        }
    }
}
