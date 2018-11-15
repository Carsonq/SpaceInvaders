using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SBNodeLink : DLink { } 

    public class SBNode : SBNodeLink
    {
        private SpriteBase pSpriteBase;
        private SBNodeMan pBackSBNodeMan;

        public SBNode()
            : base()
        {
            this.pSpriteBase = null;  // SBNode does not own pSpriteBase
            this.pBackSBNodeMan = null;
        }

        //public void Set(GameSprite.Name name)
        //{
        //    this.pSpriteBase = GameSpriteMan.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}

        //public void Set(BoxSprite.Name name)
        //{
        //    this.pSpriteBase = BoxSpriteMan.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}

        //public void Set(ProxySprite pNode)
        //{
        //    Debug.Assert(pNode != null);
        //    this.pSpriteBase = pNode;
        //}

        //public void Set(SpriteBase pNode)
        //{
        //    Debug.Assert(pNode != null);
        //    this.pSpriteBase = pNode;
        //}

        public void Set(SpriteBase pNode, SBNodeMan _pSBNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSBNode(this);

            Debug.Assert(_pSBNodeMan != null);
            this.pBackSBNodeMan = _pSBNodeMan;
        }

        public SBNodeMan GetSBNodeMan()
        {
            Debug.Assert(this.pBackSBNodeMan != null);
            return this.pBackSBNodeMan;
        }

        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSBNodeMan != null);
            return this.pBackSBNodeMan.GetSpriteBatch();
        }

        public void Wash()
        {
            this.pSpriteBase = null;
            base.Clear();
        }

        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }
    }
}
