using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationSprite : Command
    {
        private GameSprite pSprite;
        private SLink pCurrImage;
        private SLink poHeadImage;

        public AnimationSprite(GameSprite.Name spriteName)
        {
            this.pSprite = GameSpriteMan.Find(spriteName);
            Debug.Assert(this.pSprite != null);
            this.pCurrImage = null;
            this.poHeadImage = null;
        }

        public void Attach(Image.Name imageName)
        {
            Image pImage = ImageMan.Find(imageName);
            Debug.Assert(pImage != null);
            ImageHolder pImageHolder = new ImageHolder(pImage);
            Debug.Assert(pImageHolder != null);

            SLink.AddToFront(ref this.poHeadImage, pImageHolder);
            this.pCurrImage = pImageHolder;
        }

        override public void UpdateRange(int delta) { }

        public override void Execute(float deltaTime, bool repeat)
        {
            ImageHolder pImageHolder = (ImageHolder)SLink.GetNext(this.pCurrImage);
            if (pImageHolder == null)
            {
                pImageHolder = (ImageHolder)poHeadImage;
            }

            this.pCurrImage = pImageHolder;

            this.pSprite.SwapImage(pImageHolder.GetpImange());

            if (repeat == true)
            {
                if (pSprite.name == GameSprite.Name.Squid)
                {
                    TimerMan.Add(TimerEvent.Name.SquidAnimation, this, deltaTime);
                }
                else if (pSprite.name == GameSprite.Name.Crab)
                {
                    TimerMan.Add(TimerEvent.Name.CrabAnimation, this, deltaTime);
                }
                else if (pSprite.name == GameSprite.Name.Octopus)
                {
                    TimerMan.Add(TimerEvent.Name.OctopusAnimation, this, deltaTime);
                }
                else if (pSprite.name == GameSprite.Name.ShipDies)
                {
                    TimerMan.Add(TimerEvent.Name.AnimShip, this, deltaTime);
                }
            }
        }
    }
}
