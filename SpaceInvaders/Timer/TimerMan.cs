using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class TimerMan : Manager
    {
        private static TimerMan pInstance = null;
        private TimerEvent poNodeCompare;
        private float mCurrTime;

        private TimerMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new TimerEvent();
        }

        private static TimerMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static void Destroy()
        {
            // Get the instance
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->TimerMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", TimerMan.pInstance, TimerMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            TimerMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(pInstance == null);

            pInstance = new TimerMan(reserveNum, reserveGrow);
        }

        public static TimerEvent Add(TimerEvent.Name timeName, Command pCommand, float deltaTimeToTrigger, bool repeat = true)
        {
            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            TimerMan pTimerMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTimerMan != null);

            float triggerTime = deltaTimeToTrigger + pTimerMan.mCurrTime;
            TimerEvent pPreNode = PrivLocateNode(triggerTime);
            
            TimerEvent pNode = (TimerEvent)pTimerMan.BaseAddToPosition(pPreNode);

            Debug.Assert(pNode != null);
            pNode.Set(timeName, pCommand, deltaTimeToTrigger, pTimerMan.mCurrTime, repeat);

            return pNode;
        }

        public static void UpdateEvent(float deltaTimeToTrigger)
        {
            TimerMan pTimerMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTimerMan != null);
            TimerEvent pTimerEvent = (TimerEvent)pTimerMan.BaseGetActive();

            while (pTimerEvent != null)
            {
                pTimerEvent.SetDeltaTime(deltaTimeToTrigger * pTimerEvent.GetDeltaTime());
                pTimerEvent = (TimerEvent)pTimerEvent.pNext;
            }
        }

        public static void UpdateMovementRange(int delta)
        {
            TimerMan pTimerMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTimerMan != null);
            TimerEvent pTimerEvent = (TimerEvent)pTimerMan.BaseGetActive();

            while (pTimerEvent != null)
            {
                pTimerEvent.GetCommand().UpdateRange(delta);
                pTimerEvent = (TimerEvent)pTimerEvent.pNext;
            }
        }

        private static TimerEvent PrivLocateNode(float triggerTime)
        {
            TimerMan pTimerMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTimerMan != null);
            TimerEvent pNode = (TimerEvent)pTimerMan.BaseGetActive();
            TimerEvent pPrevNode = null;

            while (pNode != null && pNode.GetTriggerTime() < triggerTime)
            {
                pPrevNode = pNode;
                pNode = (TimerEvent)pNode.pNext;
            }

            return pPrevNode;
        }

        public static void Update(float totalTime)
        {
            TimerMan pTimerMan = TimerMan.PrivGetInstance();
            Debug.Assert(pTimerMan != null);

            pTimerMan.mCurrTime = totalTime;

            TimerEvent pEvent = (TimerEvent)pTimerMan.BaseGetActive();
            TimerEvent pNextEvent = null;

            while (pEvent != null && (pTimerMan.mCurrTime >= pEvent.GetTriggerTime()))
            {
                pNextEvent = (TimerEvent)pEvent.pNext;
                if (pTimerMan.mCurrTime >= pEvent.GetTriggerTime())
                {
                    pEvent.Process();
                    pTimerMan.BaseRemove(pEvent);
                }

                pEvent = pNextEvent;
            }
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new TimerEvent();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            TimerEvent pDataA = (TimerEvent)pLinkA;
            TimerEvent pDataB = (TimerEvent)pLinkB;

            return pDataA.GetName() == pDataB.GetName();
        }

        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimerEvent pNode = (TimerEvent)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimerEvent pData = (TimerEvent)pLink;
            pData.Dump();
        }

        public static void Dump()
        {
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }
    }
}
