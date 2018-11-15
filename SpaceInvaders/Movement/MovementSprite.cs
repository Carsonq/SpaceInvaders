using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MovementSprite : Command
    {
        private Composite pSprites;
        public SLink poHead;
        private SLink pCurr;

        public MovementSprite(Composite pGrid)
        {
            this.pSprites = pGrid;
            Debug.Assert(this.pSprites != null);
            this.poHead = null;
            this.pCurr = null;
        }

        public void Attach(float x, float y, SndObserver pSn)
        {
            PositionHoder pPositionHoder = null;
            pPositionHoder = new PositionHoder(x, y, pSn);
            Debug.Assert(pPositionHoder != null);

            SLink.AddToFront(ref this.poHead, pPositionHoder);
            this.pCurr = pPositionHoder;
        }

        override public void UpdateRange(int delta)
        {
            PositionHoder pPositionHoder = (PositionHoder)poHead;
            while (pPositionHoder != null)
            {
                pPositionHoder.SetPositionX(pPositionHoder.GetPositionX()+delta);
                pPositionHoder = (PositionHoder)SLink.GetNext(pPositionHoder);
            }
        }

        public override void Execute(float deltaTime, bool repeat)
        {
            PositionHoder pPositionHoder = (PositionHoder)SLink.GetNext(this.pCurr);
            if (pPositionHoder == null)
            {
                pPositionHoder = (PositionHoder)poHead;
            }

            this.pCurr = pPositionHoder;

            this.pSprites.Move(pPositionHoder.GetPositionX(), pPositionHoder.GetPositionY());
            pPositionHoder.GetSnd().Notify();

            TimerMan.Add(TimerEvent.Name.AlienMovement, this, deltaTime);
        }
    }
}