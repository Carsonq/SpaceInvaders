using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ProxySpriteMan_MLink : Manager
    {
        public ProxySprite_Base poActive;
        public ProxySprite_Base poReserve;
    }

    class ProxySpriteMan : ProxySpriteMan_MLink
    {
        private static ProxySpriteMan pInstance = null;
        private ProxySprite poNodeCompare;

        private ProxySpriteMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new ProxySprite();
        }


        ~ProxySpriteMan()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~GameSpriteMan():{0}", this.GetHashCode());
#endif
            //this.poNodeCompare = null;
            //ProxySpriteMan.pInstance = null;
        }

        public static void Destroy()
        {
            // Get the instance
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->GameSpriteMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", GameSpriteMan.pInstance, GameSpriteMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            ProxySpriteMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            //Debug.Assert(pInstance == null);
            if (pInstance == null)
            {
                pInstance = new ProxySpriteMan(reserveNum, reserveGrow);
            }
        }

        public static ProxySprite Add(GameSprite.Name name)
        {
            ProxySpriteMan pMan = ProxySpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            ProxySprite pNode = (ProxySprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);

            return pNode;
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new ProxySprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            ProxySprite pDataA = (ProxySprite)pLinkA;
            ProxySprite pDataB = (ProxySprite)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }

        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            ProxySprite pNode = (ProxySprite)pLink;
            pNode.Wash();
        }
        
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            ProxySprite pData = (ProxySprite)pLink;
            pData.Dump();
        }

        private static ProxySpriteMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
