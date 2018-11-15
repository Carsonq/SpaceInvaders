using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileGroup : Composite
    {
        public MissileGroup(GameObject.Name name, GameSprite.Name spriteName, int index, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        ~MissileGroup()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~MissileGroup():{0}", this.GetHashCode());
#endif
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an MissileGroup
            // Call the appropriate collision reaction            
            other.VisitMissileGroup(this);
        }

        public override void Update()
        {
            // Go to first child
            //Component pComponent = ForwardIterator.GetSibling(this);
            //pComponent = ForwardIterator.GetChild(pComponent);

            base.BaseUpdateBoundingBox(this);

            base.Update();
        }
    }
}
