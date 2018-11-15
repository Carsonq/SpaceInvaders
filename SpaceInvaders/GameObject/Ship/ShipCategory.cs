using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShipCategory : Leaf
    {
        protected ShipCategory.Type type;

        public enum Type
        {
            Ship,
            ShipRoot,
            Unitialized
        }

        protected ShipCategory(GameObject.Name name, GameSprite.Name spriteName, ShipCategory.Type shipType)
            : base(name, spriteName)
        {
            this.type = shipType;
        }

        // Data: ---------------
        ~ShipCategory()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~ShipCategory():{0}", this.GetHashCode());
#endif
        }
    }
}