using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BumperCategory : Leaf
    {
        private BumperCategory.Type type;

        public enum Type
        {
            BumperGroup,
            BumperLeft,
            BumperRight,
            Unitialized
        }

        protected BumperCategory(GameObject.Name name, GameSprite.Name spriteName, BumperCategory.Type type)
            : base(name, spriteName)
        {
            this.type = type;
        }

        ~BumperCategory()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~WallCategory():{0}", this.GetHashCode());
#endif
        }

        public BumperCategory.Type GetCategoryType()
        {
            return this.type;
        }
    }
}