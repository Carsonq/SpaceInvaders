using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;
        private SBNode pBackSBNode;

        public SpriteBase()
            : base()
        {
            this.pBackSBNode = null;
        }

        public void BaseClear()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }

        public void BaseSet(float x, float y, float sx, float sy, float angle)
        {
            this.x = x;
            this.y = y;
            this.sx = sx;
            this.sy = sy;
            this.angle = angle;
        }

        public SBNode GetSBNode()
        {
            Debug.Assert(this.pBackSBNode != null);
            return this.pBackSBNode;
        }

        public void SetSBNode(SBNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pBackSBNode = pSpriteBatchNode;
        }

        abstract public void Update();
        abstract public void Render();
    }
}
