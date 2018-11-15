using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Manager
    {
        private DLink poActive;
        private DLink poReserve;
        private int mDeltaGrow;
        private int mTotalNumNodes;
        private int mNumReserved;
        private int mNumActive;

        abstract protected DLink DerivedCreateNode();
        abstract protected void DerivedWash(DLink pLink);
        abstract protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB);
        abstract protected void DerivedDumpNode(DLink pLink);
        virtual protected void DerivedDestroyNode(DLink pLink)
        {
            // default: do nothing
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pLink, pLink.GetHashCode());
#endif
            pLink = null;
        }

        protected Manager()
        {
            this.mDeltaGrow = 0;
            this.mNumReserved = 0;
            this.mNumActive = 0;
            this.mTotalNumNodes = 0;
            this.poActive = null;
            this.poReserve = null;
        }

        ~Manager()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("      ~Manager():{0}", this.GetHashCode());
#endif
            this.poActive = null;
            this.poReserve = null;
        }

        protected void BaseDestroy()
        {
            // search for the name
            DLink pNode;
            DLink pTmpNode;

            // Active List
            pNode = this.poActive;
            while (pNode != null)
            {
                // walking through the list
                pTmpNode = pNode;
                pNode = pNode.pNext;

                // node to cleanup
                Debug.Assert(pTmpNode != null);
                this.DerivedDestroyNode(pTmpNode);
                DLink.RemoveNode(ref this.poActive, pTmpNode);
                pTmpNode = null;

                this.mNumActive--;
                this.mTotalNumNodes--;
            }

            // Reserve List
            pNode = this.poReserve;
            while (pNode != null)
            {
                // walking through the list
                pTmpNode = pNode;
                pNode = pNode.pNext;

                // node to cleanup
                Debug.Assert(pTmpNode != null);
                this.DerivedDestroyNode(pTmpNode);
                DLink.RemoveNode(ref this.poReserve, pTmpNode);
                pTmpNode = null;

                this.mNumReserved--;
                this.mTotalNumNodes--;
            }
        }

        protected void BaseInitialize(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // Check now or pay later
            Debug.Assert(InitialNumReserved >= 0);
            Debug.Assert(DeltaGrow > 0);

            this.mDeltaGrow = DeltaGrow;

            // Preload the reserve
            this.PrivFillReservedPool(InitialNumReserved);
        }

        private void PrivFillReservedPool(int count)
        {
            // doesn't make sense if its not at least 1
            Debug.Assert(count > 0);

            this.mTotalNumNodes += count;
            this.mNumReserved += count;

            // Preload the reserve
            for (int i = 0; i < count; i++)
            {
                DLink pNode = this.DerivedCreateNode();
                Debug.Assert(pNode != null);

                DLink.AddToFront(ref this.poReserve, pNode);
            }
        }

        protected DLink BaseAdd()
        {
            if (this.poReserve == null)
            {
                this.PrivFillReservedPool(this.mDeltaGrow);
            }

            DLink pLink = DLink.PullFromFront(ref this.poReserve);
            Debug.Assert(pLink != null);

            // Wash it
            this.DerivedWash(pLink);

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            // copy to active
            DLink.AddToFront(ref this.poActive, pLink);

            // YES - here's your new one (its recycled from reserved)
            return pLink;
        }

        
        protected DLink BaseAddToPosition(DLink pNode)
        {
            if (this.poReserve == null)
            {
                this.PrivFillReservedPool(this.mDeltaGrow);
            }

            DLink pLink = DLink.PullFromFront(ref this.poReserve);
            Debug.Assert(pLink != null);

            // Wash it
            this.DerivedWash(pLink);

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            // copy to active
            if (pNode == null)
            {
                DLink.AddToFront(ref this.poActive, pLink);
            }
            else
            {
                DLink.AddToBehind(ref pNode, pLink);
            }

            // YES - here's your new one (its recycled from reserved)
            return pLink;
        }

        protected void BaseSetReserve(int reserveNum, int reserveGrow)
        {
            this.mDeltaGrow = reserveGrow;

            if (reserveNum > this.mNumReserved)
            {
                // Preload the reserve
                this.PrivFillReservedPool(reserveNum - this.mNumReserved);
            }
        }

        protected void BaseRemove(DLink pNode)
        {
            Debug.Assert(pNode != null);
            DLink.RemoveNode(ref this.poActive, pNode);

            this.DerivedWash(pNode);

            DLink.AddToFront(ref this.poReserve, pNode);

            this.mNumActive--;
            this.mNumReserved++;
        }

        public DLink BaseGetActive()
        {
            return this.poActive;
        }

        public void BaseSetActive(DLink pHead)
        {
            this.poActive = pHead;
        }

        public DLink BaseGetReserve()
        {
            return this.poReserve;
        }

        public void BaseFillReservedPool()
        {
            this.PrivFillReservedPool(this.mDeltaGrow);
        }

        protected DLink BaseFind(DLink pNodeTarget)
        {
            // search the active list
            DLink pLink = this.poActive;

            // Walk through the nodes
            while (pLink != null)
            {
                if (this.DerivedCompare(pLink, pNodeTarget))
                {
                    // found it
                    break;
                }
                pLink = pLink.pNext;
            }

            return pLink;
        }

        protected void BaseDump()
        {
            this.BaseDumpStats();
            this.BaseDumpNodes();
        }

        protected void BaseDumpNodes()
        {
            Debug.WriteLine("");
            Debug.WriteLine("------ Active List: ---------------------------\n");

            DLink pNode = this.poActive;
            int i;

            if (pNode == null)
            {
                Debug.WriteLine("  <list empty>");
            }
            else
            {
                i = 0;
                while (pNode != null)
                {
                    Debug.WriteLine("{0}: -------------", i);
                    this.DerivedDumpNode(pNode);
                    i++;
                    pNode = pNode.pNext;
                }
            }
            Debug.WriteLine("");
            Debug.WriteLine("------ Reserve List: ---------------------------\n");

            pNode = this.poReserve;

            if (pNode == null)
            {
                Debug.WriteLine("  <list empty>");
            }
            else
            {
                i = 0;
                while (pNode != null)
                {
                    Debug.WriteLine("{0}: -------------", i);
                    this.DerivedDumpNode(pNode);
                    i++;
                    pNode = pNode.pNext;
                }
            }
        }
        protected void BaseDumpStats()
        {
            Debug.WriteLine("");
            Debug.WriteLine("------------ Stats: -------------");
            Debug.WriteLine("  Total Num Nodes: {0}", this.mTotalNumNodes);
            Debug.WriteLine("       Num Active: {0}", this.mNumActive);
            Debug.WriteLine("     Num Reserved: {0}", this.mNumReserved);
            Debug.WriteLine("       Delta Grow: {0}", this.mDeltaGrow);
        }
    }
}
