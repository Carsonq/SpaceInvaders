using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SLink
    {
        private SLink pNext;

        public SLink()
        {
            this.pNext = null;
        }

        public static void AddToFront(ref SLink pHead, SLink pNode)
        {
            Debug.Assert(pNode != null);

            if (pHead == null)
            {
                pHead = pNode;
                pNode.pNext = null;
            }
            else
            {
                pNode.pNext = pHead;
                pHead = pNode;
            }

            Debug.Assert(pHead != null);
        }

        public static SLink GetNext(SLink pNode)
        {
            Debug.Assert(pNode != null);
            return pNode.pNext;
        }
    }
}
