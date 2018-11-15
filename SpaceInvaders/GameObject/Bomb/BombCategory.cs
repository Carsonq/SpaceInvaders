using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BombCategory : Leaf
    {
        protected BombCategory.Type type;

        public enum Type
        {
            Bomb,
            BombRoot,
            Unitialized
        }

        protected BombCategory(GameObject.Name name, GameSprite.Name spriteName, BombCategory.Type bombType)
            : base(name, spriteName)
        {
            this.type = bombType;
        }

        // Data: ---------------
        ~BombCategory()
        {
        }
    }
}