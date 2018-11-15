using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Iterator
    {
        abstract public Component Next();
        abstract public bool IsDone();
        abstract public Component First();

        static public Component GetParent(Component pNode)
        {
            Debug.Assert(pNode != null);

            return pNode.pParent;

        }
        static public Component GetChild(Component pNode)
        {
            Debug.Assert(pNode != null);

            Component pChild;

            if (pNode.holder == Component.Container.COMPOSITE)
            {
                pChild = pNode.GetFirstChild();
            }
            else
            {
                pChild = null;
            }

            return pChild;
        }
        static public Component GetSibling(Component pNode)
        {
            Debug.Assert(pNode != null);

            return (Component)pNode.pNext;
        }

//        static public GameObject GetChildGameObject(GameObject pNode)
//        {
//            GameObject pGameObj = null;

//            // TRICKY... 
//            Component pComponent = ForwardIterator.GetSibling(pNode);

//            while (pComponent != null)
//            {

//                pComponent = ForwardIterator.GetChild(pComponent);

//                if (pComponent.holder == Component.Container.LEAF)
//                {
//                    pGameObj = (GameObject)pComponent;
//                    break;
//                }
//            }

//            return pGameObj;
//        }

//        static public GameObject GetSiblingGameObject(GameObject pNode)
//        {
//            GameObject pGameObj = null;

//            Component pComponent = ForwardIterator.GetSibling(pNode);

//            while (pComponent != null)
//            {
//                if (pComponent.holder == Component.Container.LEAF)
//                {
//                    pGameObj = (GameObject)pComponent;
//                    break;
//                }
//                pComponent = ForwardIterator.GetParent(pNode);
//                pComponent = ForwardIterator.GetSibling(pComponent);
//                if (pComponent != null)
//                {
//                    pComponent = ForwardIterator.GetChild(pComponent);
//                }
//            }

//            return pGameObj;
//        }

//static public GameObject GetParentGameObject(GameObject pNode)
//        {
//            GameObject pGameObj = null;
//            Component pComponent = ForwardIterator.GetParent(pNode);
//            if (pComponent.holder == Component.Container.LEAF)
//            {
//                pGameObj = (GameObject)pComponent;
//            }

//            return pGameObj;
//        }
    }
}