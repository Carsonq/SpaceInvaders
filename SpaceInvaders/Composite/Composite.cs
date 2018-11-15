using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Composite : GameObject
    {
        public DLink poHead;
        public DLink poLast;

        public Composite(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {
            this.poHead = null;
            this.poLast = null;

            this.holder = Container.COMPOSITE;
        }

        override public void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.AddToLast(ref this.poHead, ref this.poLast, pComponent);
            pComponent.pParent = this;
        }

        override public void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            //DLink.RemoveNode(ref this.poHead, pComponent);
            DLink.RemoveNode(ref this.poHead, ref this.poLast, pComponent);
        }

        public override void Print()
        {
            DLink pNode = this.poHead;

            while(pNode != null)
            {
                Component pComponent = (Component)pNode;
                pComponent.Print();

                pNode = pNode.pNext;
            }
        }

        //public override void Move(float x, float y)
        //{
        //    DLink pNode = this.poHead;
            
        //    while (pNode != null)
        //    {
        //        Component pComponent = (Component)pNode;
        //        pComponent.Move(x, y);
        //        pNode = pNode.pNext;
        //    }
        //}

        override public Component GetFirstChild()
        {
            DLink pNode = this.poHead;
            //Debug.Assert(pNode != null);

            return (Component)pNode;
        }

        override public void DumpNode()
        {
            Debug.WriteLine(" GameObject Name:({0})  <---- Composite", this.GetHashCode());
        }

        public override void Accept(ColVisitor other)
        {
            Debug.Assert(false);
            throw new NotImplementedException();
        }
    }
}
