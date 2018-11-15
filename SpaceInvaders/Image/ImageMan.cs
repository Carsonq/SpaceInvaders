using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageMan : Manager
    {
        private static ImageMan pInstance = null;  // singleton
        private Image poNodeCompare;

        private ImageMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new Image();
        }

        ~ImageMan()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~ImageMan():{0}", this.GetHashCode());
#endif
            //this.poNodeCompare = null;
            //ImageMan.pInstance = null;
        }

        public static void Destroy()
        {
            // Get the instance
            ImageMan pMan = ImageMan.PrivGetInstance();
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->ImageMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", ImageMan.pInstance, ImageMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            ImageMan.pInstance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new ImageMan(reserveNum, reserveGrow);
                ImageMan.Add(Image.Name.NullObject, Texture.Name.NullObject, 0, 0, 128, 128);
                ImageMan.Add(Image.Name.Default, Texture.Name.Default, 0, 0, 128, 128);
            }
        }

        private static ImageMan PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static Image Add(Image.Name ImageName, Texture.Name TextureName, float x, float y, float width, float height)
        {
            ImageMan pMan = ImageMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Image pNode = (Image)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Texture pTexture = TextureMan.Find(TextureName);

            //Debug.Assert(pTexture != null);
            if (pTexture == null)
            {
                pTexture = TextureMan.Find(Texture.Name.Default);
                Debug.Assert(pTexture != null);
                x = 0;
                y = 0;
                width = 128;
                height = 128;
            }

            pNode.Set(ImageName, pTexture, x, y, width, height);

            return pNode;
        }

        public static Image Find(Image.Name name)
        {
            ImageMan pMan = ImageMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            Image pData = (Image)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Dump()
        {
            ImageMan pMan = ImageMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Image();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Image pDataA = (Image)pLinkA;
            Image pDataB = (Image)pLinkB;

            return pDataA.name == pDataB.name;
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Image pNode = (Image)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Image pData = (Image)pLink;
            pData.Dump();
        }
    }
}
