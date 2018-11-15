using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class UFOCategory : Leaf
    {
        protected UFOCategory.Type type;

        public enum Type
        {
            UFO,
            UFORoot,
            Unitialized
        }

        protected UFOCategory(GameObject.Name name, GameSprite.Name spriteName, UFOCategory.Type UFOType)
            : base(name, spriteName)
        {
            this.type = UFOType;
        }

        // Data: ---------------
        ~UFOCategory()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~ShipCategory():{0}", this.GetHashCode());
#endif
        }
    }
}