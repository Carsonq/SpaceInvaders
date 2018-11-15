using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : DLink
    {
        public enum Name
        {
            Default,
            Aliens,
            Shields,
            Consolas20pt,
            Consolas36pt,
            NullObject,
            Uninitialized
        }

        public Texture()
            : base()
        {
            Debug.Assert(Texture.psDefaultAzulTexture != null);
            this.poAzulTexture = psDefaultAzulTexture;
            Debug.Assert(this.poAzulTexture != null);
            this.name = Name.Default;
        }

        public new void Clear()
        {
            this.name = Name.Uninitialized;
        }

        public void Set(Name name, String pTextureName)
        {
            this.name = name;

            Debug.Assert(pTextureName != null);
            Debug.Assert(this.poAzulTexture != null);

            // No deletion happens here. psDefaultAzulTexture is static
            this.poAzulTexture = new Azul.Texture(pTextureName);
            Debug.Assert(this.poAzulTexture != null);
        }

        public Texture.Name GetName()
        {
            return this.name;
        }

        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.poAzulTexture != null);
            return this.poAzulTexture;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            if (this.poAzulTexture != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.name);
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
                Texture pTmp = (Texture)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Texture pTmp = (Texture)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        private static Azul.Texture psDefaultAzulTexture = new Azul.Texture("HotPink.tga");
        public Azul.Texture poAzulTexture;
        public Name name;
    }
}
