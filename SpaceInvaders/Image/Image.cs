using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Image : DLink
    {
        public enum Name
        {
            Default,
            SquidA,
            SquidB,
            CrabA,
            CrabB,
            OctopusA,
            OctopusB,
            UFO,
            UFODies,
            Missile,
            MissileBombDies,
            Ship,
            ShipDiesA,
            ShipDiesB,
            AlienDies,
            BombStraight,
            BombZigZag,
            BombCross,
            BombRoll,
            BombFork,
            MissileBombCol,

            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            NullObject,
            Uninitialized
        }

        public Name name;
        public Texture pTexture;
        public Azul.Rect poRect;

        public Image()
            : base()
        {
            this.poRect = new Azul.Rect();
            Debug.Assert(this.poRect != null);

            this.Clear();
        }

        private new void Clear()  // private, since there is no place invoke this method outside this class 
        {
            this.pTexture = null; // does not own pTexture
            this.name = Name.Uninitialized;
            this.poRect.Clear();
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.poRect != null);
            return this.poRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public void Set(Image.Name name, Texture pTexture, float x, float y, float width, float height)
        {
            // There is no new here
            this.name = name;

            Debug.Assert(pTexture != null);
            this.pTexture = pTexture;

            this.poRect.Set(x, y, width, height);
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            if (this.pTexture != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.pTexture.name);
            }
            else
            {
                Debug.WriteLine("   Texture: null ");
            }


            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    }
}