using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatchLink : DLink {}
    public class SpriteBatch : SpriteBatchLink
    {
        // Data -------------------------------
        public SpriteBatch.Name name;
        private SBNodeMan pSBNodeMan;
        private bool isDraw;

        public enum Name
        {
            Aliens,
            Boxes,
            Shields,
            Texts,
            Uninitialized
        }

        public SpriteBatch()
            : base()
        {
            this.name = SpriteBatch.Name.Uninitialized;
            this.pSBNodeMan = new SBNodeMan();
            Debug.Assert(this.pSBNodeMan != null);
            this.isDraw = true;
        }

        public void Set(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            this.name = name;
            this.pSBNodeMan.Set(name, reserveNum, reserveGrow);
            if (this.name == SpriteBatch.Name.Boxes)
            {
                this.isDraw = false;
            }
        }

        public bool GetisDraw()
        {
            return this.isDraw;
        }

        public void SetisDraw()
        {
            this.isDraw = this.isDraw == false ? true : false;
        }

        //public void Attach(GameSprite.Name name)
        //{
        //    Debug.Assert(this.pSBNodeMan != null);
        //    this.pSBNodeMan.Attach(name);
        //}

        //public void Attach(BoxSprite.Name name)
        //{
        //    Debug.Assert(this.pSBNodeMan != null);
        //    this.pSBNodeMan.Attach(name);
        //}

        //public void Attach(ProxySprite pNode)
        //{
        //    Debug.Assert(this.pSBNodeMan != null);
        //    SBNode pSBNode = this.pSBNodeMan.Attach(pNode);
        //}

        public void Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);

            // Go to Man, get a node from reserve, add to active, return it
            SBNode pSBNode = (SBNode)this.pSBNodeMan.Attach(pNode);
            Debug.Assert(pSBNode != null);

            // Initialize SpriteBatchNode
            pSBNode.Set(pNode, this.pSBNodeMan);

            this.pSBNodeMan.SetSpriteBatch(this);
        }

        public new void Clear()
        {
            name = SpriteBatch.Name.Uninitialized;
        }

        public void SetName(SpriteBatch.Name inName)
        {
            this.name = inName;
        }

        public Name GetName()
        {
            return this.name;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        public SBNodeMan GetSBNodeMan()
        {
            return this.pSBNodeMan;
        }

        public void Destroy()
        {
            Debug.Assert(this.pSBNodeMan != null);
            this.pSBNodeMan.Destroy();
        }
    }
}
