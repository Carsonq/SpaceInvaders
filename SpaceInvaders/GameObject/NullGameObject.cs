using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class NullGameObject : Leaf
    {
        public NullGameObject()
            : base(GameObject.Name.NullObject, GameSprite.Name.NullObject) { }

        ~NullGameObject()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~NullGameObject():{0}", this.GetHashCode());
#endif
        }

        override public void Update() { }
        //override public void Move(float x, float y) { }
        public override void Accept(ColVisitor other)
        {
            other.VisitNullGameObject(this);
        }
    }
} 