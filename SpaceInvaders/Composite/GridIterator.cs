using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GridIterator
    {
        private Component pCurr;
        private Component pRoot;
        private bool bIsDone;

        public GridIterator(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.holder == Component.Container.COMPOSITE);

            this.pCurr = pStart;
            this.pRoot = pStart;
            this.bIsDone = false;
        }

        private Component PrivGetParent(Component pNode)
        {
            Debug.Assert(pNode != null);

            return pNode.pParent;

        }
        private Component PrivGetChild(Component pNode)
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
        private Component PrivGetSibling(Component pNode)
        {
            Debug.Assert(pNode != null);

            return (Component)pNode.pNext;
        }


        private Component PrivNextStep(Component pNode, Component pParent, Component pSibling)
        {
            if (pSibling != null)
            {
                pNode = pSibling;
                while (pNode.holder == Component.Container.COMPOSITE)
                {
                    pNode = PrivGetChild(pNode);
                }
            }
            else
            {
                while (pParent != null)
                {
                    pNode = pParent;

                    if (pNode.pNext != null)
                    {
                        pNode = (Component)pNode.pNext;

                        while (pNode.holder == Component.Container.COMPOSITE)
                        {
                            pNode = PrivGetChild(pNode);
                        }

                        break;

                    }
                    else
                    {
                        // go up a level.
                        pParent = PrivGetParent(pNode);
                    }
                }
            }

            // Are you Done?
            if ((pNode.holder != Component.Container.LEAF) && (pParent == null))
            {
                pNode = null;
                this.bIsDone = true;
            }

            return pNode;
        }

        public Component First()
        {
            Debug.Assert(this.pRoot != null);
            Component pNode = this.pRoot;

            while (pNode.holder == Component.Container.COMPOSITE)
            {
                pNode = PrivGetChild(pNode);
            }

            Debug.Assert(pNode != null);
            this.pCurr = pNode;

            Debug.WriteLine("---> {0} ", this.pCurr.GetHashCode());
            return this.pCurr;
        }

        public Component Next()
        {
            Debug.Assert(this.pCurr != null);
            Debug.Assert(this.pCurr.holder == Component.Container.LEAF);

            Component pNode = this.pCurr;

            Component pChild = PrivGetChild(pNode);
            Component pSibling = PrivGetSibling(pNode);
            Component pParent = PrivGetParent(pNode);

            // Start - Depth first iteration

            pNode = PrivNextStep(pNode, pParent, pSibling);

            this.pCurr = pNode;
            
            if (this.pCurr != null)
            {
                Debug.WriteLine("---> {0}", this.pCurr.GetHashCode());
            }
            else
            {
                Debug.WriteLine("---> null");
            }

            return this.pCurr;
        }

        public Component CurrentItem()
        {
            return null;
        }

        public bool IsDone()
        {
            return this.bIsDone;
        }
    }
}
