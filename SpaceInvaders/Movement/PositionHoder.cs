using System;
using System.Diagnostics;

namespace SpaceInvaders {
    class PositionHoder : SLink
    {
        private float x;
        private float y;
        private SndObserver pSn;

        public PositionHoder(float x, float y, SndObserver pSn)
            :base()
        {
            this.x = x;
            this.y = y;
            this.pSn = pSn;
        }

        public float GetPositionX()
        {
            return this.x;
        }

        public void SetPositionX(float newX)
        {
            this.x = newX;
        }

        public float GetPositionY()
        {
            return this.y;
        }

        public void SetPositionY(float newY)
        {
            this.y = newY;
        }

        public SndObserver GetSnd()
        {
            return this.pSn;
        }

        public void SetSnd(SndObserver newSnd)
        {
            this.pSn = newSnd;
        }
    }
}
