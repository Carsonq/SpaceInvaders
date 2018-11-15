using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ProxySprite_Base : SpriteBase { }

    public class ProxySprite : ProxySprite_Base
    {
        public enum Name
        {
            Proxy,
            Uninitialized
        }

        //public float x;
        //public float y;
        public ProxySprite.Name name;
        public GameSprite pSprite;

        public ProxySprite()
            : base()
        {
            this.Clear();
        }

        public ProxySprite(GameSprite.Name name)
        {
            this.Clear();
            this.Set(name);
        }

        ~ProxySprite()
        {
#if (TRACK_DESTRUCTOR)   
            Debug.WriteLine("~ProxySprite():{0} ", this.GetHashCode());
#endif
            this.Clear();
        }

        private new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = null;
            this.name = Name.Uninitialized;
        }

        public void Set(GameSprite.Name name)
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.name = ProxySprite.Name.Proxy;
            this.pSprite = GameSpriteMan.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        public override void Update()
        {
            //Debug.Write(this.x);
            this.PrivPushToReal();
            this.pSprite.Update();
        }

        public override void Render()
        {
            this.PrivPushToReal();
            this.Update();
            this.pSprite.Render();
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        public void SetName(Name name)
        {
            this.name = name;
        }

        public Name GetName()
        {
            return this.name;
        }

        private void PrivPushToReal()
        {
            Debug.Assert(this.pSprite != null);

            this.pSprite.x = this.x;
            this.pSprite.y = this.y;
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }

        public void Dump()
        {

            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            
            if (this.pSprite != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.pSprite.name);
            }
            else
            {
                Debug.WriteLine("   Texture: null ");
            }


            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    }
}
