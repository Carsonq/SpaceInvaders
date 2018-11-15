using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObject : Component
    {
        public enum Name
        {
            Squid,
            Crab,
            Octopus,
            UFO,
            UFORoot,
            AlienColumn,
            AlienGroup,
            Missile,
            MissileGroup,
            WallGroup,
            WallLeft,
            WallRight,
            WallTop,
            WallBottom,
            Ship,
            ShipRoot,
            BombRoot,
            Bomb,
            ShieldRoot,
            ShieldBrick,
            ShieldColumn_0,
            ShieldColumn_1,
            ShieldColumn_2,
            ShieldColumn_3,
            ShieldColumn_4,
            ShieldColumn_5,
            ShieldColumn_6,

            BumperGroup,
            BumperRight,
            BumperLeft,

            ExplosionGroup,
            AlienDies,
            MissileDies,
            ShipDies,
            BombDies,
            UFODies,
            MissileBombCol,

            NullObject,
            Uninitialized
        }

        public float x;
        public float y;
        private GameObject.Name name;
        public bool bMarkForDeath;
        public ProxySprite pProxySprite;
        public ColObject poColObj;
        
        //protected GameObject(GameObject.Name gameName)
        //{
        //    this.x = 0.0f;
        //    this.y = 0.0f;
        //    this.name = gameName;
        //    moveDirection = 1;
        //    this.pProxySprite = null;
        //}

        protected GameObject(GameObject.Name gameName, GameSprite.Name spriteName)
        {
            this.name = gameName;
            this.bMarkForDeath = false;
            this.x = 0.0f;
            this.y = 0.0f;

            this.pProxySprite = ProxySpriteMan.Add(spriteName);
            this.poColObj = new ColObject(this.pProxySprite);
            Debug.Assert(this.poColObj != null);
        }

        public void SetXY(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float GetX()
        {
            return this.x;
        }

        public float GetY()
        {
            return this.y;
        }

        public virtual void Update()
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.x = this.x;
            this.pProxySprite.y = this.y;

            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);
            Debug.Assert(this.poColObj.pColSprite != null);
            // push the data onto the sprite
            this.poColObj.pColSprite.Update();
        }

        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.pProxySprite);
        }

        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.poColObj != null);
            pSpriteBatch.Attach(this.poColObj.pColSprite);
        }

        public GameObject.Name GetName()
        {
             return this.name;
        }

        public void SetName(GameObject.Name name)
        {
            this.name = name;
        }

        override public void Move(float speedX, float speedY)
        {
            Debug.Assert(false);
        }

        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            ColRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            pNode = (GameObject)Iterator.GetChild(pNode);

            if (pNode != null)
            {
                // Initialized the union to the first block
                ColTotal.Set(pNode.poColObj.poColRect);

                // loop through sliblings
                while (pNode != null)
                {
                    ColTotal.Union(pNode.poColObj.poColRect);

                    // go to next sibling
                    pNode = (GameObject)Iterator.GetSibling(pNode);
                }

                //this.poColObj.poColRect.Set(201, 201, 201, 201);
                this.x = this.poColObj.poColRect.x;
                this.y = this.poColObj.poColRect.y;

                //  Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", ColTotal.x, ColTotal.y, ColTotal.width, ColTotal.height);
            }
        }

        public ColObject GetColObject()
        {
            Debug.Assert(this.poColObj != null);
            return this.poColObj;
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.pProxySprite != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.pProxySprite.name);
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.pProxySprite.pSprite.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);
        }

        public virtual void Remove()
        {
            // Very difficult at first... if you are messy, you will pay here!
            // Given a game object....

            // Remove from SpriteBatch

            // Find the SBNode
            Debug.Assert(this.pProxySprite != null);
            SBNode pSBNode = this.pProxySprite.GetSBNode();

            // Remove it from the manager
            Debug.Assert(pSBNode != null);
            SpriteBatchMan.Remove(pSBNode);

            // Remove collision sprite from spriteBatch

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            pSBNode = this.poColObj.pColSprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            SpriteBatchMan.Remove(pSBNode);

            // Remove from GameObjectMan

            GameObjectMan.Remove(this);

            //GhostMan.Add(this);
        }

        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);

            this.poColObj.pColSprite.SetLineColor(red, green, blue);
        }

        //public void ToggleColColor()
        //{
        //    this.poColObj.pColSprite.ToggleColor();
        //}
    }
}
