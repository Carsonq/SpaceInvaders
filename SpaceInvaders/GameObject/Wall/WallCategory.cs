using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class WallCategory : Leaf
    {
        private WallCategory.Type type;

        public enum Type
        {
            WallGroup,
            Right,
            Left,
            Bottom,
            Top,
            Unitialized
        }

        protected WallCategory(GameObject.Name name, GameSprite.Name spriteName, WallCategory.Type type)
            : base(name, spriteName)
        {
            this.type = type;
        }

        // Data: ---------------
        ~WallCategory()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~WallCategory():{0}", this.GetHashCode());
#endif
        }

        public WallCategory.Type GetCategoryType()
        {
            return this.type;
        }
    }
}