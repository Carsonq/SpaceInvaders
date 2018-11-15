using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatchMan_Link : Manager
    {
        public SpriteBatchLink poActive;
        public SpriteBatchLink poReserve;
    }
    class SpriteBatchMan : SpriteBatchMan_Link
    {        
        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static SpriteBatchMan pInstance = null;
        private SpriteBatch poNodeCompare;

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private SpriteBatchMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point SpriteBatchMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new SpriteBatch();
        }

        ~SpriteBatchMan()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SpriteBatchMan():{0} ", this.GetHashCode());
#endif
            //SpriteBatchMan.pInstance = null;
            //this.poNodeCompare = null;
        }

        public static void Destroy()
        {
            // Get the instance
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->SpriteBatchMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", SpriteBatchMan.pInstance, SpriteBatchMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            SpriteBatchMan.pInstance = null;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new SpriteBatchMan(reserveNum, reserveGrow);
            }
        }

        public static SpriteBatch Add(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pNode = (SpriteBatch)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, reserveNum, reserveGrow);
            return pNode;
        }

        private static SpriteBatch Add(SpriteBatch.Name name, SpriteBatch pSB)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pNode = (SpriteBatch)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode = pSB;

            return pSB;
        }

        public static SpriteBatch Replace(SpriteBatch.Name name, SpriteBatch pSB)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.SetName(name);

            SpriteBatch pData = (SpriteBatch)pMan.BaseFind(pMan.poNodeCompare);
            SpriteBatch tmp = pData;
            SpriteBatchMan.Remove(pData);
            SpriteBatchMan.Add(name, pSB);

            return tmp;
        }

        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.SetName(name);

            SpriteBatch pData = (SpriteBatch)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(SpriteBatch pNode)
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Remove(SBNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SBNodeMan pSBNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSBNodeMan != null);
            pSBNodeMan.Remove(pSpriteBatchNode);
        }

        public static void Draw()
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // walk through the list and render
            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.BaseGetActive();

            while (pSpriteBatch != null)
            {
                SBNodeMan pSBNodeMan = pSpriteBatch.GetSBNodeMan();
                Debug.Assert(pSBNodeMan != null);

                if (pSpriteBatch.GetisDraw())
                {
                    pSBNodeMan.Draw();
                }

                pSpriteBatch = (SpriteBatch)pSpriteBatch.pNext;
                //SBNode pNode = (SBNode)pSBNodeMan.BaseGetActive();

                //while (pNode != null)
                //{
                //    // Assumes someone before here called update() on each sprite
                //    // Draw me.
                //    pNode.getSpriteBase().Render();

                //    pNode = (SBNode)pNode.pNext;
                //}

                //pSpriteBatch = (SpriteBatch)pSpriteBatch.pNext;
            }

        }

        public static void Update()
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);
            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.BaseGetActive();

            while (pSpriteBatch != null)
            {
                SBNodeMan pSBNodeMan = pSpriteBatch.GetSBNodeMan();
                Debug.Assert(pSBNodeMan != null);

                SBNode pNode = (SBNode)pSBNodeMan.BaseGetActive();

                while (pNode != null)
                {
                    pNode.GetSpriteBase().Update();

                    pNode = (SBNode)pNode.pNext;
                }

                pSpriteBatch = (SpriteBatch)pSpriteBatch.pNext;
            }

        }

        public static void Dump()
        {
            SpriteBatchMan pMan = SpriteBatchMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }


        public static void ToggleColBox()
        {
            SpriteBatch pSB_Box = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            pSB_Box.SetisDraw();
        }

        public static void ToggleShield()
        {
            SpriteBatch pSB_Box = SpriteBatchMan.Find(SpriteBatch.Name.Shields);
            pSB_Box.SetisDraw();
        }
        
        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new SpriteBatch();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteBatch pDataA = (SpriteBatch)pLinkA;
            SpriteBatch pDataB = (SpriteBatch)pLinkB;

            return pDataA.GetName() == pDataB.GetName();
        }
        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pNode = (SpriteBatch)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pData = (SpriteBatch)pLink;
            //pData.Dump();
        }

        override protected void DerivedDestroyNode(DLink pLink)
        {
            // default: do nothing
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pLink, pLink.GetHashCode());
#endif
            SpriteBatch pNode = (SpriteBatch)pLink;
            Debug.Assert(pNode != null);
            pNode.Destroy();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static SpriteBatchMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
