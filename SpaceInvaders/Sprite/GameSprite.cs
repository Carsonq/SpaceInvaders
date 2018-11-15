using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameSprite : SpriteBase
    {
        public Name name;
        public Image pImage;
        public Azul.Sprite poAzulSprite;
        private Azul.Rect psTmpRect = new Azul.Rect();
        private static Azul.Color psTmpColor = new Azul.Color();

        public enum Name
        {
            Squid,
            Crab,
            Octopus,
            UFO,
            Missile,
            Ship,
            Wall,
            Bumper,

            BombDagger,
            BombStraight,
            BombZigZag,
            BombRolling,
            BombFork,

            Brick,
            Brick_LeftTop0,
            Brick_LeftTop1,
            Brick_LeftBottom,
            Brick_RightTop0,
            Brick_RightTop1,
            Brick_RightBottom,

            AlienDies,
            MissileDies,
            ShipDies,
            BombDies,
            UFODies,
            MissileBombCol,

            NullObject,
            Uninitialized
        }

        public GameSprite()
            : base()   // <--- Delegate (kick the can)
        {
            this.name = GameSprite.Name.Uninitialized;

            // Use the default - it will be replaced in the Set
            this.pImage = ImageMan.Find(Image.Name.Default);
            Debug.Assert(this.pImage != null);

            Debug.Assert(this.psTmpRect != null);
            this.psTmpRect.Clear();
            Debug.Assert(GameSprite.psTmpColor != null);
            this.ClearTmpColor();

            // here is the actual new
            this.poAzulSprite = new Azul.Sprite(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.psTmpRect, psTmpColor);
            Debug.Assert(this.poAzulSprite != null);

            this.BaseSet(poAzulSprite.x, poAzulSprite.y, poAzulSprite.sx, poAzulSprite.sy, poAzulSprite.angle);
        }

        public new void Clear()   // the new is there to shut up warning
        {
            this.pImage = null;
            this.name = GameSprite.Name.Uninitialized;

            this.BaseClear();
        }

        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }

        public void Set(GameSprite.Name name, Image pImage, float x, float y, float width, float height, Azul.Color pColor)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(this.psTmpRect != null);
            Debug.Assert(this.poAzulSprite != null);

            this.psTmpRect.Set(x, y, width, height);
            this.pImage = pImage;
            this.name = name;

            if (pColor == null)
            {
                this.ClearTmpColor();
                this.poAzulSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.psTmpRect, psTmpColor);
            }
            else
            {
                this.poAzulSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.psTmpRect, pColor);
            }
            Debug.Assert(this.poAzulSprite != null);

            this.BaseSet(poAzulSprite.x, poAzulSprite.y, poAzulSprite.sx, poAzulSprite.sy, poAzulSprite.angle);
        }

        public override void Update()
        {
            this.poAzulSprite.x = this.x;
            this.poAzulSprite.y = this.y;
            this.poAzulSprite.sx = this.sx;
            this.poAzulSprite.sy = this.sy;
            this.poAzulSprite.angle = this.angle;

            this.poAzulSprite.Update();
        }

        public override void Render()
        {
            this.poAzulSprite.Render();
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Image: {0} ({1})", this.pImage.name, this.pImage.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSprite.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);


            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        public GameSprite.Name GetName()
        {
            return this.name;
        }

        private void ClearTmpColor()
        {
            GameSprite.psTmpColor.Set(1, 1, 1, 1);
        }

        public void SwapPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(pNewImage != null);
            this.pImage = pNewImage;

            this.poAzulSprite.SwapTexture(this.pImage.GetAzulTexture());
            this.poAzulSprite.SwapTextureRect(this.pImage.GetAzulRect());
        }

        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.psTmpRect != null);
            return this.psTmpRect;
        }
    }
}