using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GhostGameObjectMan_MLink : Manager
    {
        public GameObjectNode_Link poActive;
        public GameObjectNode_Link poReserve;
    }

    class GhostGameObjectMan : GameObjectMan_MLink
    {
        private static GhostGameObjectMan pInstance = null;
        private GameObjectNode poNodeCompare;
        private NullGameObject poNullGameObject;

        private GhostGameObjectMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new GameObjectNode();
            this.poNullGameObject = new NullGameObject();
            this.poNodeCompare.Set(this.poNullGameObject);
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(pInstance == null);
            pInstance = new GhostGameObjectMan(reserveNum, reserveGrow);
        }

        public static void Destroy()
        {
            // Get the instance
            GhostGameObjectMan pMan = GhostGameObjectMan.PrivGetInstance();
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
            GhostGameObjectMan.pInstance = null;
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            GhostGameObjectMan pMan = GhostGameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode;
            if (pMan.BaseGetActive() == null)
            {
                pNode = (GameObjectNode)pMan.BaseAdd();
            }
            else
            {
                pNode = (GameObjectNode)pMan.BaseAddToPosition(pMan.BaseGetActive());
            }

            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObjectNode Find(GameObject.Name name)
        {
            GhostGameObjectMan pMan = GhostGameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes
            pMan.poNodeCompare.pGameObj.SetName(name);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseFind(pMan.poNodeCompare);
            
            if (pNode == null)
            {
                return null;
            }

            return pNode;
        }

        public static void Remove(GameObjectNode pNode)
        {
            GhostGameObjectMan pMan = GhostGameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        private static GhostGameObjectMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new GameObjectNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameObjectNode pDataA = (GameObjectNode)pLinkA;
            GameObjectNode pDataB = (GameObjectNode)pLinkB;

            return pDataA.GetGameObj().GetName() == pDataB.GetGameObj().GetName();
        }

        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pNode = (GameObjectNode)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pData = (GameObjectNode)pLink;
            pData.Dump();
        }
    }
}
