using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObjectMan_MLink : Manager
    {
        public GameObjectNode_Link poActive;
        public GameObjectNode_Link poReserve;
    }

    class GameObjectMan : GameObjectMan_MLink
    {
        private static GameObjectMan pInstance = null;
        private GameObjectNode poNodeCompare;
        private NullGameObject poNullGameObject;

        private GameObjectMan(int reserveNum = 3, int reserveGrow = 1)
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
            pInstance = new GameObjectMan(reserveNum, reserveGrow);
        }

        public static void Destroy()
        {
            // Get the instance
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
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
            GameObjectMan.pInstance = null;
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObject Find(GameObject.Name name)
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes
            pMan.poNodeCompare.pGameObj.SetName(name);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseFind(pMan.poNodeCompare);
            Debug.Assert(pNode != null);

            return pNode.pGameObj;
        }

        public static void Remove(GameObjectNode pNode)
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Remove(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();

            GameObject pSafetyNode = pNode;

            // OK so we have a linked list of trees (Remember that)

            // 1) find the tree root (we already know its the most parent)

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)Iterator.GetParent(pTmp);
            }

            // 2) pRoot is the tree we are looking for
            // now walk the active list looking for pRoot

            GameObjectNode pTree = (GameObjectNode)pMan.BaseGetActive();

            while (pTree != null)
            {
                if (pTree.pGameObj == pRoot)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pTree = (GameObjectNode)pTree.pNext;
            }

            // 3) pTree is the tree that holds pNode
            //  Now remove the node from that tree

            Debug.Assert(pTree != null);
            Debug.Assert(pTree.pGameObj != null);

            // Is pTree.poGameObj same as the node we are trying to delete?
            // Answer: should be no... since we always have a group (that was a good idea)

            Debug.Assert(pTree.pGameObj != pNode);

            GameObject pParent = (GameObject)Iterator.GetParent(pNode);
            Debug.Assert(pParent != null);

            GameObject pChild = (GameObject)Iterator.GetChild(pNode);
            Debug.Assert(pChild == null);

            // remove the node
            pParent.Remove(pNode);

            // Still need to optimize
        }

        public static void Update()
        {
            GameObjectMan pMan = GameObjectMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pGameObjectNode = (GameObjectNode)pMan.BaseGetActive();

            while (pGameObjectNode != null)
            {
                //Debug.WriteLine("update: GameObjectTree {0} ({1})", pGameObjectNode.pGameObj, pGameObjectNode.pGameObj.GetHashCode());
                //Debug.WriteLine("   +++++");
                // Need to rework GameObjectMan to only use Components
                ReverseIterator pRev = new ReverseIterator(pGameObjectNode.pGameObj);

                Component pNode = pRev.First();
                while (!pRev.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;
                    pGameObj.Update();

                    pNode = pRev.Next();
                }

                pGameObjectNode = (GameObjectNode)pGameObjectNode.pNext;
            }
        }

        private static GameObjectMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        //public static void ToggelColBox()
        //{
        //    SpriteBatch pSB_Box = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

        //    //Debug.Assert(pInstance != null);
        //    //GameObjectNode pGameObjectNode = (GameObjectNode)pInstance.BaseGetActive();
        //    //GameObjectMan.SetShowColBox();

        //    //while (pGameObjectNode != null)
        //    //{
        //    //    ReverseIterator pRev = new ReverseIterator(pGameObjectNode.pGameObj);

        //    //    Component pNode = pRev.First();
        //    //    while (!pRev.IsDone())
        //    //    {
        //    //        GameObject pGameObj = (GameObject)pNode;
        //    //        pGameObj.ToggleColColor();

        //    //        pNode = pRev.Next();
        //    //    }

        //    //    pGameObjectNode = (GameObjectNode)pGameObjectNode.pNext;
        //    //}
        //}

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
