using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BoxSprite : SpriteBase
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Box,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        public Azul.Color poLineColor;
        private Azul.SpriteBox poAzulSpriteBox;
        //private Azul.Color poLineColorHolder;

        //---------------------------------------------------------------------------------------------------------
        // Static Data - prevent unecessary "new" in the above methods
        //---------------------------------------------------------------------------------------------------------
        static private Azul.Rect psTmpRect = new Azul.Rect();
        static private Azul.Color psTmpColor = new Azul.Color();

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public BoxSprite()
            : base()   // <--- Delegate (kick the can)
        {
            this.name = BoxSprite.Name.Uninitialized;

            Debug.Assert(BoxSprite.psTmpRect != null);
            this.ClearTmpRect();
            Debug.Assert(BoxSprite.psTmpColor != null);
            this.ClearTmpColor();

            // Here is the actual new
            this.poAzulSpriteBox = new Azul.SpriteBox(psTmpRect, psTmpColor);
            Debug.Assert(this.poAzulSpriteBox != null);

            // Here is the actual new
            //this.poLineColor = new Azul.Color(0, 0, 0);
            this.poLineColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.poLineColor != null);
            //this.poLineColorHolder = new Azul.Color(0, 0, 0);

            this.BaseSet(poAzulSpriteBox.x, poAzulSpriteBox.y, poAzulSpriteBox.sx, poAzulSpriteBox.sy, poAzulSpriteBox.angle);
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------

        //public void ToggleColor()
        //{
        //    if (GameObjectMan.GetShowColBox() == true)
        //    {
        //        this.poLineColor.Set(this.poLineColorHolder);
        //    }
        //    else
        //    {
        //        this.poLineColor.Set(BoxSprite.psTmpColor);
        //    }
        //}

        public void Set(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pLineColor)
        {
            Debug.Assert(this.poAzulSpriteBox != null);
            Debug.Assert(this.poLineColor != null);

            Debug.Assert(psTmpRect != null);
            BoxSprite.psTmpRect.Set(x, y, width, height);

            this.name = name;

            if (pLineColor == null)
            {
                this.poLineColor.Set(0, 0, 0);
            }
            else
            {
                this.poLineColor.Set(pLineColor);
            }

            //this.poLineColorHolder = poLineColor;
            this.poAzulSpriteBox.Swap(psTmpRect, this.poLineColor);
            Debug.Assert(this.poAzulSpriteBox != null);

            this.BaseSet(poAzulSpriteBox.x, poAzulSpriteBox.y, poAzulSpriteBox.sx, poAzulSpriteBox.sy, poAzulSpriteBox.angle);
        }

        private void Set(BoxSprite.Name boxName, float x, float y, float width, float height)
        {
            Debug.Assert(this.poAzulSpriteBox != null);
            Debug.Assert(this.poLineColor != null);

            Debug.Assert(psTmpRect != null);
            BoxSprite.psTmpRect.Set(x, y, width, height);

            this.name = boxName;

            this.poAzulSpriteBox.Swap(psTmpRect, this.poLineColor);
            Debug.Assert(this.poAzulSpriteBox != null);

            this.BaseSet(poAzulSpriteBox.x, poAzulSpriteBox.y, poAzulSpriteBox.sx, poAzulSpriteBox.sy, poAzulSpriteBox.angle);
        }

        public void SetScreenRect(float x, float y, float width, float height)
        {
            this.Set(this.name, x, y, width, height);
        }

        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.name = BoxSprite.Name.Uninitialized;
            this.poLineColor.Set(1, 1, 1);
            //this.poLineColorHolder.Set(0, 0, 0);
            this.BaseClear();
        }

        public void SwapColor(Azul.Color _pColor)
        {
            Debug.Assert(_pColor != null);
            this.poAzulSpriteBox.SwapColor(_pColor);
        }

        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      Color(r,b,g): {0},{1},{2} ({3})", this.poLineColor.red, this.poLineColor.green, this.poLineColor.blue, this.poLineColor.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSpriteBox.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);


            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                BoxSprite pTmp = (BoxSprite)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                BoxSprite pTmp = (BoxSprite)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        public override void Update()
        {
            this.poAzulSpriteBox.x = this.x;
            this.poAzulSpriteBox.y = this.y;
            this.poAzulSpriteBox.sx = this.sx;
            this.poAzulSpriteBox.sy = this.sy;
            this.poAzulSpriteBox.angle = this.angle;

            this.poAzulSpriteBox.Update();
        }

        public override void Render()
        {
            this.poAzulSpriteBox.Render();
        }

        private void ClearTmpColor()
        {
            BoxSprite.psTmpColor.Set(0, 0, 0, 0);
        }

        private void ClearTmpRect()
        {
            BoxSprite.psTmpRect.Set(0, 0, 1, 1);
        }

        public void SetLineColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.poLineColor != null);
            this.poLineColor.Set(red, green, blue, alpha);

            //this.poLineColorHolder.Set(red, green, blue, alpha);
            //if (GameObjectMan.GetShowColBox() == true)
            //{
            //    this.poLineColor.Set(red, green, blue, alpha);
            //}
        }

    }
}